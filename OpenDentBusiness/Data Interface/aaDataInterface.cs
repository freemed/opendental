/*Classes in the DataInterface folder handle queries.  This is the only place in the program where queries should be.  All classes should end in "s". Every single method needs to have one of the following at the top of the method:

1. If applicable:
//No need to check RemotingRole; no call to db.
 
or 2. Something similar to one of these examples:

if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
	return Meth.GetLong(MethodBase.GetCurrentMethod(),patNum,dateStart);
}
or
if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
	Meth.GetVoid(MethodBase.GetCurrentMethod());
	return;
}
or
if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
	return Meth.GetTable(MethodBase.GetCurrentMethod(),insPlan);
}
if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
	return Meth.GetObject<Patient>(MethodBase.GetCurrentMethod(),patNum,isGuarantor,lName);
} 
or
if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) { 
	return Meth.GetObject<Adjustment[]>(MethodBase.GetCurrentMethod(),patNum);
}
or
if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
	return Meth.GetObject<List<LabCase>>(MethodBase.GetCurrentMethod(),startDate,endDate);
}
 
Not allowed to pass arrays as parameters; use lists instead.  The use of public static variables is strongly discouraged, because if passed off to the server, then the client will not have access to them.  Public static variables are only used in the cache pattern, where a great deal of thought has gone into the synch process.


*/