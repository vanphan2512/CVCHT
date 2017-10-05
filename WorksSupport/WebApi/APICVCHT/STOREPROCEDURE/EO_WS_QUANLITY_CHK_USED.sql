CREATE OR REPLACE PROCEDURE ERP.EO_WS_QUANLITY_CHK_USED
/*
	CREATED BY	:	NGUYEN VAN HUAN 
	DATE		    :	04/10/2017
	DESCRIPTION	:	KIEM TRA CHAT LUONG DA DUOC SU DUNG 
*/
(   
    V_WORKSSUPPORTQUALITYID EO_WORKSSUPPORTSTATUS.WORKSSUPPORTSTATUSID%TYPE,
    V_OUT OUT NUMBER    
) 
AS
   V_IS_USED NUMBER := 0;
BEGIN
   BEGIN
	 SELECT COUNT(1) INTO V_IS_USED
  	 FROM EO_WORKSSUPPORTQUALITY  E_QUANLITY
     WHERE E_QUANLITY.WORKSSUPPORTQUALITYID = V_WORKSSUPPORTQUALITYID
           AND E_QUANLITY.ISDELETED =0
           AND  EXISTS (SELECT E_WORK.WORKSSUPPORTID
                          FROM EO_WORKSSUPPORT E_WORK
                         WHERE E_WORK.WORKSSUPPORTQUALITYID = E_QUANLITY.WORKSSUPPORTQUALITYID
                               AND E_WORK.ISDELETED = 0
                               AND EXISTS (SELECT E_GROUP.WORKSSUPPORTGROUPID
                                              FROM EO_WORKSSUPPORTGROUP E_GROUP
                                             WHERE E_GROUP.WORKSSUPPORTGROUPID = E_WORK.WORKSSUPPORTGROUPID
                                                   AND E_GROUP.ISDELETED =0
                                                   AND EXISTS (SELECT E_PROJECT.WORKSSUPPORTPROJECTID
                                                                 FROM EO_WORKSSUPPORTPROJECT E_PROJECT
                                                                WHERE E_PROJECT.WORKSSUPPORTPROJECTID = E_GROUP.WORKSSUPPORTPROJECTID
                                                                      AND E_PROJECT.ISDELETED =0
                                                                      AND EXISTS (SELECT E_TYPE.WORKSSUPPORTTYPEID
                                                                                    FROM EO_WORKSSUPPORTTYPE E_TYPE
                                                                                   WHERE E_PROJECT.WORKSSUPPORTTYPEID = E_TYPE.WORKSSUPPORTTYPEID
                                                                                         AND E_TYPE.ISDELETED =0
                                                                                  )
                                                                )
                                              )
                        );
    EXCEPTION WHEN OTHERS THEN V_IS_USED := 0;
    END;
    V_OUT := V_IS_USED;
END;
/