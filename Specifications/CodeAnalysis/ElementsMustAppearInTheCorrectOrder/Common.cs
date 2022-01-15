namespace Aksio.CodeAnalysis.ElementsMustAppearInTheCorrectOrder
{
    public static class Common
    {
        public const string ValidOrder = @"
                class Blabla
                {
                    string _someBacking;
                    public string BackedField => _someBacking.Replace(""a"", ""b"");
                    public int Teller { get; private set; }
                    public delegate void SomethingHappenedEventHandler(object sender, object args);
                    public event EventHandler SomethingHappened;

                    public Blabla()
                    {
                        Teller = 0;
                        _someBacking = ""meh"";
                    }

                    ~Blabla()
                    {
                        Teller = int.MinValue;
                    }

                    public int this[int i] => Teller;

                    public void GjørNoeRart()
                    {
                        _someBacking = $""{ØkTeller()}"";
                    }

                    void ØkTeller() => ++Teller;

                    // Private properties should be ignored
                    int TellerPlus1 => Teller+1;
                }   
            ";
    }
}