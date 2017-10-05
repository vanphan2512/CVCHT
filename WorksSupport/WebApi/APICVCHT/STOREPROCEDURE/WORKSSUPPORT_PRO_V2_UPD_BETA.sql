CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORT_PRO_V2_UPD_BETA
/*
	Created by	:	Nghiem Huu Quang 
	Date		:	10/22/2013
	Description	:	C?p nh?t thông tin Ti?n d? công vi?c c?n h? tr? 
*/
(
	v_ProgressID IN EO_WorksSupport_Progress.ProgressID%TYPE,
	v_WorksSupportID IN EO_WorksSupport_Progress.WorksSupportID%TYPE,
	v_ProgressValue IN EO_WorksSupport_Progress.ProgressValue%TYPE,
	v_WorksSupportStatusID IN EO_WorksSupport_Progress.WorksSupportStatusID%TYPE,
	v_UpdatedUser IN EO_WorksSupport_Progress.UpdatedUser%TYPE,
	v_LogType IN EO_WorksSupport_Progress.LogType%TYPE,
	v_UserHostAddress IN EO_WorksSupport_Progress.UserHostAddress%TYPE,
	v_CertificateString IN EO_WorksSupport_Progress.CertificateString%TYPE,
	v_LoginLogID IN EO_WorksSupport_Progress.LoginLogID%TYPE
) 
AS
BEGIN
	UPDATE EO_WorksSupport_Progress
	SET
		WorksSupportID	= v_WorksSupportID,
		ProgressValue	= v_ProgressValue,
		WorksSupportStatusID	= v_WorksSupportStatusID,
		UpdatedUser	= v_UpdatedUser,
		UpdatedDate	= SYSDATE,
		LogType	= v_LogType,
		UserHostAddress	= v_UserHostAddress,
		CertificateString	= v_CertificateString,
		LoginLogID	= v_LoginLogID
	WHERE
		ProgressID = v_ProgressID
;
	
END;
/