using BeeMart.backend.Data;
using BeeMart.backend.DTO;
using BeeMart.backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeeMart.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint 1: Cari Barang (Maksimal 10)
        [HttpGet("products")]
        public IActionResult SearchProducts([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return Ok(new List<MsProduct>());

            var products = _context.MsProducts
                .Where(p => p.Name.Contains(keyword) && p.Stock > 0 && p.Stsrc == true)
                .Take(10)
                .ToList();

            return Ok(products);
        }

        // Endpoint 2: Ambil Metode Pembayaran
        [HttpGet("payment-methods")]
        public IActionResult GetPaymentMethods()
        {
            var methods = _context.LtPaymentMethods
                .Where(m => m.Stsrc == true)
                .ToList();
            return Ok(methods);
        }

        // Endpoint 3: Submit Transaksi & Generate Invoice
        [HttpPost("submit")]
        public IActionResult SubmitTransaction([FromBody] SubmitTransactionDto request)
        {
            if (request.Items == null || !request.Items.Any())
                return BadRequest(new { message = "Keranjang belanja kosong." });

            // Generate Invoice No: [5 digit userID][ddmmyy][3 digit running number]
            string dateStr = DateTime.Now.ToString("ddMMyy");

            // Cari jumlah transaksi user di hari yang sama untuk running number
            int dailyTxCount = _context.TrTransactions
                .Count(t => t.IdUser == request.IdUser && t.TransactionDate.Date == DateTime.Today);

            string runningNumber = (dailyTxCount + 1).ToString("D3");
            string invoiceNo = $"{request.IdUser}{dateStr}{runningNumber}";

            decimal totalPrice = 0;
            string newTransactionId = Guid.NewGuid().ToString();
            var transactionDetails = new List<TrTransactionDetail>();

            foreach (var item in request.Items)
            {
                var product = _context.MsProducts.FirstOrDefault(p => p.IdProduct == item.IdProduct);
                if (product == null || product.Stock < item.Qty)
                    return BadRequest(new { message = $"Stok tidak mencukupi untuk produk ID: {item.IdProduct}" });

                // Kurangi stok
                product.Stock -= item.Qty;

                decimal subTotal = (item.Price - (item.Price * (item.Discount / 100))) * item.Qty;
                totalPrice += subTotal;

                transactionDetails.Add(new TrTransactionDetail
                {
                    IdTransactionDetail = Guid.NewGuid().ToString(),
                    IdTransaction = newTransactionId,
                    IdProduct = item.IdProduct,
                    Qty = item.Qty,
                    Price = item.Price,
                    Discount = item.Discount,
                    SubTotal = subTotal,
                    Stsrc = true,
                    Datein = DateTime.Now,
                    Userin = request.IdUser
                });
            }

            var newTransaction = new TrTransaction
            {
                IdTransaction = newTransactionId,
                InvoiceNo = invoiceNo,
                TransactionDate = DateTime.Now,
                IdUser = request.IdUser,
                IdPaymentMethod = request.IdPaymentMethod,
                TotalPrice = totalPrice,
                Stsrc = true,
                Datein = DateTime.Now,
                Userin = request.IdUser
            };

            _context.TrTransactions.Add(newTransaction);
            _context.TrTransactionDetails.AddRange(transactionDetails);
            _context.SaveChanges();

            return Ok(new { message = "Transaksi berhasil disubmit", invoiceNo = invoiceNo });
        }
    }
}