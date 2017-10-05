CREATE OR REPLACE PROCEDURE EO_WORKSSUPPORTPROJECT_P_UPD
/*
	CREATED BY	:	L��NG TRUNG NH�N
	DATE		:	12/06/2017
	DESCRIPTION	:	C?P NH?T TH�NG TIN QUY?N TR�N D? �N C�NG VI?C C?N H? TR? 
*/ (
    V_WORKSSUPPORTTYPEID   IN EO_WORKSSUPPORTPROJECT_PERMIS.WORKSSUPPORTTYPEID%TYPE,
    V_USERNAME             IN EO_WORKSSUPPORTPROJECT_PERMIS.USERNAME%TYPE,
    V_ISCANADDPROJECT      IN EO_WORKSSUPPORTPROJECT_PERMIS.ISCANADDPROJECT%TYPE,
    V_ISCANEDITPROJECT     IN EO_WORKSSUPPORTPROJECT_PERMIS.ISCANEDITPROJECT%TYPE,
    V_ISCANDELETEPROJECT   IN EO_WORKSSUPPORTPROJECT_PERMIS.ISCANDELETEPROJECT%TYPE,
    V_ISCANVIEWPROJECT     IN EO_WORKSSUPPORTPROJECT_PERMIS.ISCANVIEWPROJECT%TYPE,
    V_ISCANVIEWPROJECTREPORT     IN EO_WORKSSUPPORTPROJECT_PERMIS.ISCANVIEWPROJECTREPORT%TYPE
)
    AS
BEGIN
    UPDATE EO_WORKSSUPPORTPROJECT_PERMIS
        SET
            ISCANADDPROJECT = V_ISCANADDPROJECT,
            ISCANEDITPROJECT = V_ISCANEDITPROJECT,
            ISCANDELETEPROJECT = V_ISCANDELETEPROJECT,
            ISCANVIEWPROJECT = V_ISCANVIEWPROJECT,
            ISCANVIEWPROJECTREPORT = V_ISCANVIEWPROJECTREPORT
    WHERE
        WORKSSUPPORTTYPEID = V_WORKSSUPPORTTYPEID
    AND
        USERNAME = V_USERNAME;

END;