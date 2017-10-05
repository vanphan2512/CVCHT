CREATE OR REPLACE PROCEDURE ERP.WSGROUP_MEMBER_V2_DEL
/*
	CREATED BY	:	NGUY?N TH? KIM NG�N
	DATE		:	22.07.2017
	DESCRIPTION	:	X�A TH�NG TIN TH�NH VI�N NHOM CONG VIEC 
*/
(
  V_WORKSSUPPORTGROUPID IN EO_WORKSSUPPORTGROUP_MEMBER.WORKSSUPPORTGROUPID%TYPE,
	V_MEMBERUSERNAME IN EO_WORKSSUPPORTPROJECT_MEMBER.MEMBERUSERNAME%TYPE,
	V_DELETEDUSER IN EO_WORKSSUPPORTPROJECT_MEMBER.DELETEDUSER%TYPE
)
AS
BEGIN
  --. XOA DU LIEU BANG WORKGROUP_MEMBER 
  DELETE FROM EO_WORKSSUPPORTGROUP_MEMBER
     WHERE MEMBERUSERNAME = V_MEMBERUSERNAME
     AND WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID;
END;
/