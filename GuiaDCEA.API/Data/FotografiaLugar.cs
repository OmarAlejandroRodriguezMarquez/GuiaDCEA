namespace GuiaDCEA.API.Data
{
    public class FotografiaLugar
    {
        public int Id { get; set; }
        public int LugarId { get; set; }
        public Lugar? Lugar { get; set; }
        public int FotografiaId { get; set; }
        public Fotografia? Fotografia { get; set; }
    }
}
