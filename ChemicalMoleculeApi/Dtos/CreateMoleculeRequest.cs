namespace ChemicalMoleculeApi.Dtos
{
    public class CreateMoleculeRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public string Formula { get; set; } = string.Empty;
        public double MolecularWeight { get; set; } = double.NaN;
    }
}
