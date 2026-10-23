namespace BeeMart.backend.DTO
{
    public class HistoryResponseDto
    {
        public string NoTransaksi { get; set; } = null!;
        public string TanggalJam { get; set; } = null!;
        public string Kasir { get; set; } = null!;
        public int JmlItem { get; set; }
        public string Pembayaran { get; set; } = null!;
        public decimal TotalHarga { get; set; }
    }

    public class PagedHistoryResponse
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<HistoryResponseDto> Data { get; set; } = new();
    }
}