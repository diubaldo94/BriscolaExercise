using Briscola.Tdd.Logic;

namespace Briscola.Tdd
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BriscolaRunner briscolaRunner=new BriscolaRunner();

            do { 
                briscolaRunner.Initialize();
                briscolaRunner.Play();
            } while (briscolaRunner.Continue());
        }
    }
}
