namespace GuiaDCEA.API.Data
{
    public class Lugar
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Domicilio { get; set; }
        public string Ubicacion { get; set; }
        public float Precio { get; set; }
        public bool Abierto { get; set; }
        public string Horario { get; set; }
        public string Observaciones { get; set; }
    }
}
