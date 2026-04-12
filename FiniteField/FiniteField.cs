namespace FiniteFieldService
{
    public class FiniteField
    {
        public readonly uint prime;
        public readonly uint exponent;
        FiniteField(uint prime, uint exponent)
        {
            this.prime = prime;
            this.exponent = exponent;
        }
    }
}