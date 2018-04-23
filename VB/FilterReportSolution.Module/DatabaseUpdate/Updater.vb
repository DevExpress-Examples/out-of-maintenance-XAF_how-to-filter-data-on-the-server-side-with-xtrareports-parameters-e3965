Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Reports

Namespace FilterReportSolution.Module.DatabaseUpdate
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()

			If ObjectSpace.FindObject(Of Note)(Nothing) Is Nothing Then
				For i As Integer = 0 To 9999
					Dim n As Note = ObjectSpace.CreateObject(Of Note)()
					n.Text = String.Format("sample text {0}", i + 1)
					n.Author = String.Format("author{0}", i Mod 5 + 1)
				Next i
			End If

			CreateReport("TestReport")
		End Sub
		Private Sub CreateReport(ByVal reportName As String)
			Dim reportdata As ReportData = ObjectSpace.FindObject(Of ReportData)(New BinaryOperator("Name", reportName))
			If reportdata Is Nothing Then
				reportdata = ObjectSpace.CreateObject(Of ReportData)()
				Dim rep As New XafReport()
				rep.ObjectSpace = ObjectSpace
				rep.LoadLayout(Me.GetType().Assembly.GetManifestResourceStream(Me.GetType().Namespace + "." & reportName & ".repx"))
				rep.ReportName = reportName
				reportdata.SaveXtraReport(rep)
				reportdata.IsInplaceReport = True
				reportdata.Save()
			End If
		End Sub

	End Class
End Namespace
