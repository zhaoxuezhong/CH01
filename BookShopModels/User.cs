
using System;
namespace BookShop.Models
{

    [Serializable()]
	public class User
	{

        //�û�״̬����ΪUser���Ե�����
        public UserState UserState { get; set; }
        //�û���ɫ����ΪUser���Ե�����
        public UserRole UserRole { get; set; }
        public int Id { get; set; }//���
        public string LoginId { get; set; }//��¼�û���
        public string LoginPwd { get; set; }//��¼����
        public string Name { get; set; }//����
        public string Address { get; set; }//��ַ
        public string Phone { get; set; }//�绰
        public string Mail { get; set; }//����
        public DateTime? Birthday { get; set; }  //��������


        
	}
}
