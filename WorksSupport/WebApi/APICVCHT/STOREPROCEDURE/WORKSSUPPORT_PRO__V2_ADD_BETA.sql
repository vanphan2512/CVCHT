CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORT_PRO__V2_ADD_BETA
/*
	CREATED BY	:	NGHIEM HUU QUANG 
	DATE		:	10/22/2013
	DESCRIPTION	:	THÊM THÔNG TIN TI?N D? CÔNG vI?C C?N H? TR? 
*/
(
	v_WorksSupportID IN EO_WorksSupport_Progress.WorksSupportID%TYPE,
	v_Progressvalue IN EO_WorksSupport_Progress.Progressvalue%TYPE,
	v_WorksSupportStatusID IN EO_WorksSupport_Progress.WorksSupportStatusID%TYPE,
  v_UpdatedUser IN EO_WorksSupport_Progress.UpdatedUser%TYPE,
  v_UpdatedDate IN EO_WorksSupport_Progress.UpdatedDate%TYPE,
	v_LogType IN EO_WorksSupport_Progress.LogType%TYPE,
	v_UserHostAddress IN EO_WorksSupport_Progress.UserHostAddress%TYPE,
	v_CertificateString IN EO_WorksSupport_Progress.CertificateString%TYPE,
	v_LoginLogID IN EO_WorksSupport_Progress.LoginLogID%TYPE
)
AS
BEGIN

	INSERT
	INTO EO_WorksSupport_Progress
	(
		WorksSupportID,
		Progressvalue,
		WorksSupportStatusID,
    UpdatedUser,
    UpdatedDate,
		LogType,
		UserHostAddress,
		CertificateString,
		LoginLogID
	)
	VALUES
	(
		v_WorksSupportID,
		v_Progressvalue,
		v_WorksSupportStatusID,
    v_UpdatedUser,
    v_UpdatedDate,
		v_LogType,
		v_UserHostAddress,
		v_CertificateString,
		v_LoginLogID
	);

END;
/