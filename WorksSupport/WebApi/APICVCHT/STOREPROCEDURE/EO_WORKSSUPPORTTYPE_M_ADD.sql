create or replace PROCEDURE EO_WORKSSUPPORTTYPE_M_ADD
/*
	CREATED BY	:	L��NG TRUNG NH�N
	DATE		:	12/06/2017
	DESCRIPTION	:	TH�M TH�NG TIN VAI TR? C?A M?T C�NG VI?C C?N H? TR?
*/ (
    V_WORKSSUPPORTTYPEID             IN EO_WORKSSUPPORTTYPE_MEMBERROLE.WORKSSUPPORTTYPEID%TYPE,
    V_WORKSSUPPORTMEMBERROLEID       IN EO_WORKSSUPPORTTYPE_MEMBERROLE.WORKSSUPPORTMEMBERROLEID%TYPE,
    V_ISCANADDWORKSSUPPORTGROUP      IN EO_WORKSSUPPORTTYPE_MEMBERROLE.ISCANADDWORKSSUPPORTGROUP%TYPE,
    V_ISCANADDWORKSSUPPORT           IN EO_WORKSSUPPORTTYPE_MEMBERROLE.ISCANADDWORKSSUPPORT%TYPE,
    V_ISCANEDITWORKSSUPPORTGROUP     IN EO_WORKSSUPPORTTYPE_MEMBERROLE.ISCANEDITWORKSSUPPORTGROUP%TYPE,
    V_ISCANEDITWORKSSUPPORT          IN EO_WORKSSUPPORTTYPE_MEMBERROLE.ISCANEDITWORKSSUPPORT%TYPE,
    V_ISCANDELETEWORKSSUPPORTGROUP   IN EO_WORKSSUPPORTTYPE_MEMBERROLE.ISCANDELETEWORKSSUPPORTGROUP%TYPE,
    V_ISCANDELETEWORKSSUPPORT        IN EO_WORKSSUPPORTTYPE_MEMBERROLE.ISCANDELETEWORKSSUPPORT%TYPE,
    V_ISDEFAULTROLE        IN EO_WORKSSUPPORTTYPE_MEMBERROLE.ISDEFAULTROLE%TYPE

)
    AS
BEGIN
    INSERT INTO EO_WORKSSUPPORTTYPE_MEMBERROLE (
        WORKSSUPPORTTYPEID,
        WORKSSUPPORTMEMBERROLEID,
        ISCANADDWORKSSUPPORTGROUP,
        ISCANADDWORKSSUPPORT,
        ISCANEDITWORKSSUPPORTGROUP,
        ISCANEDITWORKSSUPPORT,
        ISCANDELETEWORKSSUPPORTGROUP,
        ISCANDELETEWORKSSUPPORT,
        ISDEFAULTROLE

    ) VALUES (
        V_WORKSSUPPORTTYPEID,
        V_WORKSSUPPORTMEMBERROLEID,
        V_ISCANADDWORKSSUPPORTGROUP,
        V_ISCANADDWORKSSUPPORT,
        V_ISCANEDITWORKSSUPPORTGROUP,
        V_ISCANEDITWORKSSUPPORT,
        V_ISCANDELETEWORKSSUPPORTGROUP,
        V_ISCANDELETEWORKSSUPPORT,
        V_ISDEFAULTROLE
    );
END;