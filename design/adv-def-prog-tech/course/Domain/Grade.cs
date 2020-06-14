namespace Course
{
    public class Grade
    {
        private Grade(double numericEquivalent) { NumericEquivalent = numericEquivalent; }

        public double NumericEquivalent { get; }

        public static Grade A { get; } = new Grade(4);
        public static Grade B { get; } = new Grade(3);
        public static Grade C { get; } = new Grade(2);
        public static Grade D { get; } = new Grade(1);
        public static Grade F { get; } = new Grade(0);
    }

}
