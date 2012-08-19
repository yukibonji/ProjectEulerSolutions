#light
// These references are required only for typechecking purposes
// you can skip their evaluation.
#r "ViewletControls.dll"
#r "VSLabFSICore.dll"
#r "VSLabChartPackage.dll"

open VSLab.Chart

let gv = new GraphControl()

gv.Show()

gv.ShowProperty()

gv.Close()