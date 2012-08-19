// These references are required only for typechecking purposes
// you can skip their evaluation.
#r "ViewletControls"
#r "VSLabFSICore"
#r "VSLabShellPackage"
#r "EnvDTE80"

open VSLab.Shell

let monitor = new VSMon()
monitor.Show()

monitor.ShowProperty();

monitor.Close()
