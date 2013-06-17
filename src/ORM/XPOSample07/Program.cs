
namespace XPOSample07
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using DXample.XPOSample07.Models;

  using DevExpress.Xpo;
  using DevExpress.Xpo.DB;
  using DevExpress.Data.Filtering;

  class Program
  {
    static void Main()
    {
      //
      // XPO�ɂ́ANestedUnitOfWork�Ƃ����N���X�����݂���B
      // ���̃N���X�́A�����ʂ�l�X�g����UOW�������B
      //�i�T�u�g�����U�N�V�����̂悤�ȃC���[�W�j
      // 
      // NestedUnitOfWork���ŃR�~�b�g���ꂽ�ύX��
      // �e��UOW�ɂăR�~�b�g����Ȃ�����A�m�肵�Ȃ��B
      // ����́A�ʂ�UOW����͕ύX�������Ȃ��Ƃ����Ӗ��ł���
      // �e��UOW�����NestedUnitOfWork�̕ύX�͌�����B
      //
      // �T�O�I�ɂ́ADB�̃g�����U�N�V������TransactionScope�Ɠ����B
      //
      // ��ʂɂĕʉ�ʂ��J���āA�f�[�^��ҏW���Ă܂��߂��Ă���ꍇ�Ȃǂɗ��p�ł���B
      // (Tutorial 4�͂��̃p�^�[�����������Ă���Ă���B�j
      //
      // NestedUnitOfWork���ł́A�I�u�W�F�N�g��e��UOW�Ɩ��m�ɕ����Ď擾���邱�Ƃ��o����B
      //   �EGetNestedObject
      // �܂��ANestedUnitOfWork���Őe����UOW����I�u�W�F�N�g���擾���邱�Ƃ��o����B
      //   �EGetParentObject
      //
      // [�Q�l���\�[�X]
      //   http://documentation.devexpress.com/#XPO/CustomDocument2260
      //   http://documentation.devexpress.com/#XPO/CustomDocument2113
      //
      var dataStore = new InMemoryDataStore();
      var dataLayer = new SimpleDataLayer(dataStore);

      //
      // �����f�[�^����.
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        for (int i = 0; i < 10; i++)
        {
          new Customer(uow){ Name = string.Format("Customer-[{0}]", i), Age = i + 30 };
        }

        uow.CommitChanges();
      }

      //
      // NestedUnitOfWork���쐬���ĐeUOW���R�~�b�g���Ă��Ȃ���Ԃł̒l���m�F.
      //
      var criteria = CriteriaOperator.Parse("Age = 33");

      using (var uow = new UnitOfWork(dataLayer))
      {
        using (var nuow = uow.BeginNestedUnitOfWork())
        {
          var nuowCustomer = nuow.FindObject<Customer>(criteria);
          nuowCustomer.Name = "Modified";

          nuow.CommitChanges();
        }

        var theCustomer = uow.FindObject<Customer>(criteria);
        Console.WriteLine(theCustomer.Name);

        //
        // �킴�Ɛe��UOW�ł�CommitChanges���Ă΂��ɏ����I��.
        //
        // �ȉ��̃R�����g���O���ƁA�ύX���m�肳��A�ʂ�UOW�ł��ύX��������悤�ɂȂ�.
        //uow.CommitChanges();
      }

      //
      // �ʂ�UOW�ōēx�����������w�肵�Ēl���m�F.
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        var theCustomer = uow.FindObject<Customer>(criteria);
        Console.WriteLine(theCustomer.Name);
      }

      using (var uow = new UnitOfWork(dataLayer))
      {
        var parentCustomer = uow.FindObject<Customer>(criteria);

        using (var nuow = uow.BeginNestedUnitOfWork())
        {
          var nestedCustomer  = nuow.GetNestedObject<Customer>(parentCustomer);
          nestedCustomer.Name = "Modified 2";

          nuow.CommitChanges();
        }

        Console.WriteLine(parentCustomer.Name);
      }

      //
      // �ʂ�UOW�ōēx�����������w�肵�Ēl���m�F.
      //
      using (var uow = new UnitOfWork(dataLayer))
      {
        var theCustomer = uow.FindObject<Customer>(criteria);
        Console.WriteLine(theCustomer.Name);
      }

    }
  }
}
