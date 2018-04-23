using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Reports;

namespace FilterReportSolution.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            if (ObjectSpace.FindObject<Note>(null) == null) {
                for (int i = 0; i < 10000; i++) {
                    Note n = ObjectSpace.CreateObject<Note>();
                    n.Text = string.Format("sample text {0}", i + 1);
                    n.Author = string.Format("author{0}", i % 5 + 1);
                }
            }

            CreateReport("TestReport");
        }
        private void CreateReport(string reportName) {
            ReportData reportdata = ObjectSpace.FindObject<ReportData>(new BinaryOperator("Name", reportName));
            if (reportdata == null) {
                reportdata = ObjectSpace.CreateObject<ReportData>();
                XafReport rep = new XafReport();
                rep.ObjectSpace = ObjectSpace;
                rep.LoadLayout(GetType().Assembly.GetManifestResourceStream(this.GetType().Namespace + "." + reportName + ".repx"));
                rep.ReportName = reportName;
                reportdata.SaveReport(rep);
                reportdata.IsInplaceReport = true;
                reportdata.Save();
            }
        }

    }
}
