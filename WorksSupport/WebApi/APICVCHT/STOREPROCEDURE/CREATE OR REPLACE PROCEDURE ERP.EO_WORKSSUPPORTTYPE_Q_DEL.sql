CREATE OR REPLACE PROCEDURE ERP.EO_WORKSSUPPORTTYPE_Q_DEL
/*
	CREATED BY	:	LUONG TRUNG NH�N 
	DATE		:	02/06/2017
	DESCRIPTION	:	X�A TH�NG TIN B?NG LI�N K?T LO?I C�NG VI?C V� CH?T LU?NG C�NG VI?C
*/
(
	V_WORKSSUPPORTTYPEID IN EO_WORKSSUPPORTTYPE_QUALITY.WORKSSUPPORTTYPEID%TYPE
)
AS
BEGIN
	DELETE EO_WORKSSUPPORTTYPE_QUALITY
	WHERE
		WORKSSUPPORTTYPEID = V_WORKSSUPPORTTYPEID;
END;
/