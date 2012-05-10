using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Every user group has certain permissions.  This defines a permission for a group.  The absense of permission would cause that row to be deleted from this table.</summary>
	[Serializable]
	public class GroupPermission:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long GroupPermNum;
		///<summary>Only granted permission if newer than this date.  Can be Minimum (01-01-0001) to always grant permission.</summary>
		public DateTime NewerDate;
		///<summary>Can be 0 to always grant permission.  Otherwise, only granted permission if item is newer than the given number of days.  1 would mean only if entered today.</summary>
		public int NewerDays;
		///<summary>FK to usergroup.UserGroupNum.  The user group for which this permission is granted.  If not authorized, then this groupPermission will have been deleted.</summary>
		public long UserGroupNum;
		///<summary>Enum:Permissions</summary>
		public Permissions PermType;

		///<summary></summary>
		public GroupPermission Copy(){
			return (GroupPermission)this.MemberwiseClone();
		}

	}

	///<summary>A hard-coded list of permissions which may be granted to usergroups.</summary>
	public enum Permissions {
		///<summary>0</summary>
		None,
		///<summary>1</summary>
		AppointmentsModule,
		///<summary>2</summary>
		FamilyModule,
		///<summary>3</summary>
		AccountModule,
		///<summary>4</summary>
		TPModule,
		///<summary>5</summary>
		ChartModule,
		///<summary>6</summary>
		ImagesModule,
		///<summary>7</summary>
		ManageModule,
		///<summary>8. Currently covers a wide variety of setup functions. </summary>
		Setup,
		///<summary>9</summary>
		RxCreate,
		///<summary>10. Uses date restrictions.  Covers editing AND deleting of completed procs.  Deleting non-completed procs is covered by ProcDelete.</summary>
		ProcComplEdit,
		///<summary>11</summary>
		ChooseDatabase,
		///<summary>12</summary>
		Schedules,
		///<summary>13</summary>
		Blockouts,
		///<summary>14. Uses date restrictions.</summary>
		ClaimSentEdit,
		///<summary>15</summary>
		PaymentCreate,
		///<summary>16. Uses date restrictions.</summary>
		PaymentEdit,
		///<summary>17</summary>
		AdjustmentCreate,
		///<summary>18. Uses date restrictions.</summary>
		AdjustmentEdit,
		///<summary>19</summary>
		UserQuery,
		///<summary>20.  Not used anymore.</summary>
		StartupSingleUserOld,
		///<summary>21 Not used anymore.</summary>
		StartupMultiUserOld,
		///<summary>22</summary>
		Reports,
		///<summary>23. Includes setting procedures complete.</summary>
		ProcComplCreate,
		///<summary>24. At least one user must have this permission.</summary>
		SecurityAdmin,
		///<summary>25. </summary>
		AppointmentCreate,
		///<summary>26</summary>
		AppointmentMove,
		///<summary>27</summary>
		AppointmentEdit,
		///<summary>28</summary>
		Backup,
		///<summary>29</summary>
		TimecardsEditAll,
		///<summary>30</summary>
		DepositSlips,
		///<summary>31. Uses date restrictions.</summary>
		AccountingEdit,
		///<summary>32. Uses date restrictions.</summary>
		AccountingCreate,
		///<summary>33</summary>
		Accounting,
		///<summary>34</summary>
		AnesthesiaIntakeMeds,
		///<summary>35</summary>
		AnesthesiaControlMeds,
		///<summary>36</summary>
		InsPayCreate,
		///<summary>37. Uses date restrictions. Also includes completed claimprocs even if unattached to an insurance check.  However, it's not actually enforced when creating a check because it would be very complex.</summary>
		InsPayEdit,
		///<summary>38. Uses date restrictions.</summary>
		TreatPlanEdit,
		///<summary>39</summary>
		ReportProdInc,
		///<summary>40. Uses date restrictions.</summary>
		TimecardDeleteEntry,
		///<summary>41. Uses date restrictions. All other equipment functions are covered by .Setup.</summary>
		EquipmentDelete,
		///<summary>42. Uses date restrictions. Also used in audit trail to log web form importing.</summary>
		SheetEdit,
		///<summary>43. Uses date restrictions.</summary>
		CommlogEdit,
		///<summary>44. Uses global date restriction only.</summary>
		ImageDelete,
		///<summary>45. Uses date restrictions.</summary>
		PerioEdit,
		///<summary>46</summary>
		ProcEditShowFee,
		///<summary>47</summary>
		AdjustmentEditZero,
		///<summary>48</summary>
		EhrEmergencyAccess,
		///<summary>49. Uses date restrictions.  This only applies to non-completed procs.  Deletion of completed procs is covered by ProcComplEdit.</summary>
		ProcDelete,
		///<summary>50 - Only used at OD HQ.  No user interface.</summary>
		EhrKeyAdd,
		///<summary>51</summary>
		Providers,
		///<summary>52</summary>
		EcwAppointmentRevise,
		///<summary>53</summary>
		ProcedureNote,
		///<summary>54</summary>
		ReferralAdd,
		///<summary>55</summary>
		InsPlanChangeSubsc,
		///<summary>56</summary>
		RefAttachAdd,
		///<summary>57</summary>
		RefAttachDelete
	}

	
}













