CREATE OR REPLACE PROCEDURE ERP.SYS_USER_SRH_ALL_V2
/*
	Created by	:	Truong Trung L?i 
	Date		:	18/07/2012
	Description	:	N?p danh s�ch Ngu?i d�ng 
*/
(
    v_Out OUT SYS_REFCURSOR
) 
AS
BEGIN
    OPEN v_Out FOR
	SELECT
		USERNAME,		
    FULLNAME,
    FIRSTNAME,
    LASTNAME,		
    DEPARTMENTID,
    POSITIONID,
    WORKSTOREID,
    DEFAULTPICTUREURL,		
    DEFAULTPICTUREFILEID
	FROM SYS_USER
	WHERE ISDELETED = 0
;
END;
/