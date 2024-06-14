namespace ColorPaletteGeneratorApi.Dtos
{
    public class PaletteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BaseColor { get; set; }
        public List<PaletteColorDto> Colors { get; set; }
        public bool AlreadyCreated { get; set; }
    }
}
