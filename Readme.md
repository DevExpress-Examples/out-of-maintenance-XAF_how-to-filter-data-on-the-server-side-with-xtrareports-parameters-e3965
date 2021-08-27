<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128590465/12.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3965)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Updater.cs](./CS/FilterReportSolution.Module/DatabaseUpdate/Updater.cs) (VB: [Updater.vb](./VB/FilterReportSolution.Module/DatabaseUpdate/Updater.vb))
<!-- default file list end -->
# How to filter data on the server side with XtraReports Parameters


<p>The criteria you specify in the XtraReport.FilterString property is applied to data source rows by the report control on the client side by default. When working with large data sets, it makes sense to apply this filter directly to the data source to avoid excessive data transfer. This task can be accomplished by creating the following scripts in the report:</p>


```cs
private void xafReport1_DataSourceDemanded(object sender, System.EventArgs e) {
      DevExpress.Data.Filtering.OperandValue[] prameterOperands;
      DevExpress.Data.Filtering.CriteriaOperator criteria = DevExpress.Data.Filtering.CriteriaOperator.Parse(xafReport1.FilterString, out prameterOperands);
      foreach(DevExpress.Data.Filtering.OperandValue theOperand in prameterOperands) {
        DevExpress.Data.Filtering.OperandParameter operandParameter = theOperand as DevExpress.Data.Filtering.OperandParameter;
        if(!object.ReferenceEquals(operandParameter, null)) {
          DevExpress.XtraReports.Parameters.Parameter param = xafReport1.Parameters[operandParameter.ParameterName];
            if(param != null)
              operandParameter.Value = param.Value;
        }
      }
      DevExpress.ExpressApp.Reports.XafReport xafReport = (DevExpress.ExpressApp.Reports.XafReport)xafReport1;
      IList originalDataSource = xafReport.ObjectSpace.GetObjects(xafReport.DataType, criteria);
      xafReport1.DataSource = new DevExpress.ExpressApp.ProxyCollection(xafReport.ObjectSpace, DevExpress.ExpressApp.XafTypesInfo.Instance.FindTypeInfo(xafReport.DataType), originalDataSource);
}

```


<p> <br><strong>UPDATE:</strong><br>Note that the new <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113592.aspx">Reports V2 Module</a> provides the server-side filtering capability out of the box. Refer to the <a href="https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113594.aspx">Data Filtering in Reports V2</a> article for details.<br><br></p>

<br/>


