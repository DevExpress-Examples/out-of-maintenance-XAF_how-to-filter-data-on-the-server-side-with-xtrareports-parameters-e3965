using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using DevExpress.ExpressApp;

namespace FilterReportSolution.Module.Win {
    [ToolboxItemFilter("Xaf.Platform.Win")]
    public sealed partial class FilterReportSolutionWindowsFormsModule : ModuleBase {
        public FilterReportSolutionWindowsFormsModule() {
            InitializeComponent();
        }
    }
}
