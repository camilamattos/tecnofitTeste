using System.Collections.Generic;

namespace Tecnofit.Models
{
    public class Parametros
    {
        public List<PickerModel> formaCobranca { get; set; }
        public List<PickerModel> categoriaConta { get; set; }
        public List<PickerModel> centroResponsabilidade { get; set; }
        public List<PickerModel> contaBanco { get; set; }
    }
}
