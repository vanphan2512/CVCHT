CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORT_PRO_V2_SRH_BETA
/*
	Created by	:	Nghiem Huu Quang 
	Date		:	10/22/2013
	Description	:	N?p danh sách Ti?n d? công vi?c c?n h? tr? 
*/
(
    v_Out OUT SYS_REFCURSOR
) 
AS
BEGIN
    OPEN v_Out FOR
	SELECT
		ProgressID,
		WorksSupportID,
		ProgressValue,
		WorksSupportStatusID,
		UpdatedUser,
		UpdatedDate,
		LogType,
		UserHostAddress,
		CertificateString,
		LoginLogID
	FROM EO_WorksSupport_Progress

;
END;
/