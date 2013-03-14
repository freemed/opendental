package com.opendental.odweb.client.dbmultitable;

import com.opendental.opendentbusiness.tabletypes.Userod;

public class Security {
	private static Userod curUser;

	public static Userod getCurUser() {
		return curUser;
	}

	public static void setCurUser(Userod curUser) {
		Security.curUser = curUser;
	}

}
