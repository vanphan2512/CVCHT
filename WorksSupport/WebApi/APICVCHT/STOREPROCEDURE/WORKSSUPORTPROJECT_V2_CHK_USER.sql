CREATE OR REPLACE PROCEDURE ERP.WORKSSUPORTPROJECT_V2_CHK_USER
/*
	CREATED BY	:	NGUY?N VAN HU�N 
	DATE		:	09/08/2017
	DESCRIPTION	:	KI?M TRA TH�NH VI�N TRONG D? �N C� TH?C HI?N BU?C X? L� N�O KH�NG.
*/
(
	V_WORKSSUPPORTPROJECTID IN EO_WORKSSUPPORTPROJECT.WORKSSUPPORTPROJECTID%TYPE,
  V_PROCESSUSER IN EO_WORKSSUPPORT_WORKFLOW.PROCESSUSER%TYPE,
  V_OUT OUT SYS_REFCURSOR
)
AS
BEGIN
  OPEN V_OUT FOR
    SELECT COUNT(1) AS CHECKPROCESSUSER  
    FROM  EO_WORKSSUPPORT_WORKFLOW EWW
    WHERE
          EWW.PROCESSUSER = V_PROCESSUSER  AND EWW.ISPROCESS = 0  AND 
          EXISTS (SELECT 
                      EW.WORKSSUPPORTID 
                    FROM 
                      EO_WORKSSUPPORT EW 
                    WHERE  
                      EW.WORKSSUPPORTID = EWW.WORKSSUPPORTID AND EW.ISDELETED =0 AND
                      EXISTS (SELECT 
                                  EW1.WORKSSUPPORTGROUPID 
                               FROM 
                                  EO_WORKSSUPPORTGROUP EW1
                               WHERE 
                                  EW1.WORKSSUPPORTGROUPID = EW.WORKSSUPPORTGROUPID AND 
                                  EW1.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID AND
                                  EW1.isdeleted =0
                                )
                    );
END;
/