using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeriBeslemeliYapaySinirAg
{
    class Fonksiyonlar
    {
        public static double sigmoid(double ax)
        {
            return 1.0 / (1.0 + Math.Exp(-ax));
        }
        public static double sigmoid_türev(double ax)
        {
            return ax * (1 - ax);
        }
    }
}
