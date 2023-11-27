using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSHComponent
{
    public class LshButton : Button
    {
        public LshButton() {

            this.FlatStyle = FlatStyle.Flat; // Flat 스타일을 사용하여 테두리를 설정합니다.
            this.FlatAppearance.BorderSize = 1; // 테두리의 두께를 1로 설정합니다.
            this.FlatAppearance.BorderColor = Color.White;

        }

    }
}
