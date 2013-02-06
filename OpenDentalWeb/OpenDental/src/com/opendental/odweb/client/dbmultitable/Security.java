package com.opendental.odweb.client.dbmultitable;

import com.opendental.odweb.client.tabletypes.Userod;

public class Security {
	private static Userod curUser;

	public static Userod getCurUser() {
		return curUser;
	}

	public static void setCurUser(Userod curUser) {
		Security.curUser = curUser;
	}

}
