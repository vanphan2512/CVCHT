
  CREATE OR REPLACE PROCEDURE ERP.WSPROJECT_MEMBER_V2_GETBYID
/*
	CREATED BY	:	NGUY?N TH? KIM NG�N
	DATE		:	22/07/2017
	DESCRIPTION	: L?Y TH�NH VI�N D? �N THEO ID 
*/
(	
  V_OUT OUT SYS_REFCURSOR,
	V_WORKSSUPPORTPROJECTID IN EO_WORKSSUPPORTPROJECT_MEMBER.WORKSSUPPORTPROJECTID%TYPE
) 
AS
BEGIN
  OPEN V_OUT FOR
  SELECT
    USERS.FULLNAME,
    USERS.DEFAULTPICTUREURL,
    SD.DEPARTMENTID,
    SD.DEPARTMENTNAME,
    EO_WORKSSUPPORTPROJECT_MEMBER.WORKSSUPPORTMEMBERROLEID,
    ROLE.WORKSSUPPORTMEMBERROLENAME
  FROM
    EO_WORKSSUPPORTPROJECT_MEMBER
    LEFT JOIN SYS_USER USERS ON USERS.USERNAME = EO_WORKSSUPPORTPROJECT_MEMBER.MEMBERUSERNAME AND USERS.ISDELETED = 0
    LEFT JOIN SYS_DEPARTMENT SD ON USERS.DEPARTMENTID = SD.DEPARTMENTID  AND SD.ISDELETED = 0 
    LEFT JOIN EO_WORKSSUPPORTMEMBERROLE ROLE ON ROLE.WORKSSUPPORTMEMBERROLEID = EO_WORKSSUPPORTPROJECT_MEMBER.WORKSSUPPORTMEMBERROLEID AND ROLE.ISDELETED =0
  WHERE
    EO_WORKSSUPPORTPROJECT_MEMBER.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID
    AND EO_WORKSSUPPORTPROJECT_MEMBER.ISDELETED = 0
  ;
END;
/