create or replace PROCEDURE SOLUTION_AMT_WORKSSUPPORTID
/*
	Created by	:	L��ng Trung Nh�n
	Date		:	19/07/2017
	Description	:	N?p th�ng tin Danh s�ch file d�nh k�m c?a c�ng vi?c c?n h? tr? 
*/
(	
	V_WORKSSUPPORTID IN EO_WORKSSUPPORT_SOLUTIONATM.WORKSSUPPORTID%type,	
	V_Out OUT SYS_REFCURSOR
) 
AS
BEGIN
    OPEN v_Out FOR
	SELECT
		ATTACHMENTID,
		WORKSSUPPORTID,
		FILEPATH,
		FILENAME,
        FILEID
	FROM EO_WORKSSUPPORT_SOLUTIONATM
	WHERE
		WORKSSUPPORTID = V_WORKSSUPPORTID
  AND ISDELETED = 0;
END;