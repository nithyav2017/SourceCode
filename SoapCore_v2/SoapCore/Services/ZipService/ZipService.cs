using System;
using ZipCode.Data;
using Services.DataContract;
using System.Collections.Generic;
using System.Linq;
using OutAssemblyDataContract;

namespace Services
{
    public class ZipService : IZipService
    {
        IZipCodeRepository _ZipCodeRepository = null;

        public ZipService()
        {

        }

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        /// <param name="zipCodeRepository"></param>
        public ZipService(IZipCodeRepository zipCodeRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
        }

        public School GetSchoolInfo(string Name)
        {
            School _school = new School();
            _school.SchoolName = "Woodland Primary";
            _school.ClassName = "Brown Bear";
            _school.Teacher = "Mrs.Brown";
            return _school;
        }

        /// <summary>
        /// Get city state details for given zipcode
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public ZipCodeModel GetZipInfo(string zip)
        {
            ZipCodeModel _zipcodeData = null;
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode.Data.Entity.ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                _zipcodeData = new ZipCodeModel()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State,
                    ZIPCode = zipCodeEntity.ZIPCode,
                    Abbreviation = zipCodeEntity.Abbreviation

                };
            }
            return _zipcodeData;
        }

        /// <summary>
        /// Get list of zipcodes for the given state code
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public IEnumerable<ZipCodeModel> GetZips(string state)
        {
            List<ZipCodeModel> zipCodeData = new List<ZipCodeModel>();
            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();
            var zips = zipCodeRepository.GetByState(state);
            if (zips != null)
            {
                foreach (ZipCode.Data.Entity.ZipCode zipCode in zips)
                {
                    zipCodeData.Add(new ZipCodeModel()
                    {
                        City = zipCode.City,
                        State = zipCode.State,
                        ZIPCode = zipCode.ZIPCode,
                        Abbreviation = zipCode.Abbreviation
                    });

                }
            }
            return zipCodeData.ToList();
        }
    }
}
