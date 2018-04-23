;#DropDB FilterReportSolutionEasyTest

#Application FilterReportSolution.Win
#Application FilterReportSolution.Web

*Action Navigation(User)

*ProcessRecord
 User Name = [DomainUserName]

*OptionalAction Edit
 
*FillForm
 User Name = Test User
 
*Action Cancel

*OptionalAction Yes

*CheckFieldValues
 User Name = [DomainUserName]
