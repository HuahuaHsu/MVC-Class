using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [EnableCors("MVC")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            return _context.Employees.Select(e => new EmployeeDTO
            {
                EmployeeId = e.EmployeeId,
                LastName = e.LastName,
                FirstName = e.FirstName,
                Title = e.Title
            });
			//return await _context.Employees.ToListAsync(); //轉集合轉陣列需要時間、空間，直接回傳IEnumerable就好，讓前端決定要轉成陣列還是集合
		}

		// GET: api/Employees/5
		[HttpGet("{id}")]
        public async Task<EmployeeDTO> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return null;
            }

            return new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title
            };
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ResultDTO> PutEmployee(int id, EmployeeDTO employeeDTO)
        {
			//先判斷前端傳來的id和DTO裡的id是否相同，不同就回傳400
			if (id != employeeDTO.EmployeeId)
            {
                return new ResultDTO
                {
                    Ok = false,
                    Code = 400,
				};
            }
			//再找出資料庫裡的資料做判斷是否有這筆資料
			Employee Emp = await _context.Employees.FindAsync(id);
            if (Emp == null)
            {
                return new ResultDTO
                {
                    Ok = false,
                    Code = 404,
                };
            }
            else
			{   //有這筆資料就更新
				Emp.FirstName = employeeDTO.FirstName;
				Emp.LastName = employeeDTO.LastName;
                Emp.Title = employeeDTO.Title;
				_context.Entry(Emp).State = EntityState.Modified;

				try
				{
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					return new ResultDTO
					{
						Ok = false,
						Code = 500,
					};
				}

				return new ResultDTO
				{
					Ok = true,
					Code = 204,
				};
			}   
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
		//方法一:回傳EmployeeDTO物件
		//     public async Task<EmployeeDTO> PostEmployee(EmployeeDTO employeeDTO)
		//     {
		//         //Dto轉回Model後新增
		//         Employee Emp = new Employee
		//         {
		//             EmployeeId = 0,//Dto不知道原本Id
		//             FirstName = employeeDTO.FirstName,
		//             LastName = employeeDTO.LastName,
		//             Title = employeeDTO.Title,
		//         };
		//         _context.Employees.Add(Emp);
		//         await _context.SaveChangesAsync();//儲存後才會有EmployeeId
		//employeeDTO.EmployeeId = Emp.EmployeeId;//回傳給前端，讓前端知道新增的EmployeeId
		//         return employeeDTO; //傳回新增的物件
		//     }

		//方法二:回傳bool/伺服器狀態
		public async Task<ResultDTO> PostEmployee(EmployeeDTO employeeDTO)
		{
			//Dto轉回Model後新增
			Employee Emp = new Employee
			{
				EmployeeId = 0,//Dto不知道原本Id
				FirstName = employeeDTO.FirstName,
				LastName = employeeDTO.LastName,
				Title = employeeDTO.Title,
			};
			_context.Employees.Add(Emp);
			await _context.SaveChangesAsync();//儲存後才會有EmployeeId
			employeeDTO.EmployeeId = Emp.EmployeeId;//回傳給前端，讓前端知道新增的EmployeeId
			return new ResultDTO{
                Ok=true,
                Code = 204,
            }; //傳回新增的物件
		}

		// DELETE: api/Employees/5
		[HttpDelete("{id}")]
        public async Task<ResultDTO> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ResultDTO
                {
                    Ok = false,
                    Code = 404,
                };
			}

            try
            {
				_context.Employees.Remove(employee);
				await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
				return new ResultDTO
				{
					Ok = false,
					Code = 500,
				};
			}
            return new ResultDTO
            {
                Ok = true,
                Code = 204,
            };
        }
    }
}
