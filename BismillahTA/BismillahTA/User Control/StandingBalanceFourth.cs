using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BismillahTA.User_Control
{
    class StandingBalanceFourth : BaseUserControl
    {

        public StandingBalanceFourth() : base()
        {

        }

        public StandingBalanceFourth(MainWindow prnt) : base(prnt)
        {
            instructionLabel.Content = "Posisi berdiri pada jarak +- 3m dari Kinect";
        }

    }
}
