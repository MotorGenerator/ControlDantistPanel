use Dentists
begin transaction

-- ���������� ��� �������� ���������� ���������.
declare @countContract int
--declare @countThaun int
-- �������� ���� �� ����� ������� ��������� ��������
select @countContract = COUNT(*)  from ������� 
where ������������� = '2-2/5556' and id_�������� in 
(select id_�������� from �������� where ������� = '��������' and ��� = '����' and �������� = '��������' and ������������ =  '19360310' and ������������ = 'True') 

-- �������� ���� �� ������� ������� �� ������ ��������
if(@countContract = 0)
begin
	declare @countThaun int
	select @countThaun = count(������������) from �������������� where ������������ = '�������'
	if(@countThaun = 0)
	begin 
		INSERT INTO ��������������(������������)VALUES('�������') 
	end
	else
	declare @countRaion int
	 --�������� ���� �� � ���� ������ �����
	select @countRaion = count(������������) from ������������������ where ������������ = ''
	if(@countRaion = 0)
	begin
		
	end
	
end
else
begin 
print('�� ����� � ���� ������')
end



rollback transaction