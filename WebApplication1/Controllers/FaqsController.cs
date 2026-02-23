using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.EfModels;

namespace WebApplication1.Controllers
{
    public class FaqsController : Controller
    {
        private readonly dbFirstAppContext _context;

		//沒有Program的依賴注入(DI)，這裡就會報錯，因為沒有注入dbFirstAppContext的實例，他會不知道怎麼new出這個物件
		public FaqsController(dbFirstAppContext context)
        {
            _context = context;
        }

        // GET: Faqs
        public async Task<IActionResult> Index()
        {
            var data = await _context.Faqs.ToListAsync();
            return View(data);

            //return View(await _context.Faqs.ToListAsync());
        }

        // GET: Faqs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // GET: Faqs/Create
        public IActionResult Create()
        {
            return View();
        }

		//2:53講GET/POST
		//3:03 Model Binding 自動根據傳來的資訊，解析並把值塞進去
		//且model binding只針對properties，對於fields是無法綁定的

		// POST: Faqs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer,IsActive,DisplayOrder")] Faq faq)
        {
			//ModelState.IsValid會檢查Model Binding的過程中，是否有任何錯誤發生，例如資料類型不匹配、必填欄位缺失等，如果有錯誤，ModelState.IsValid會返回false，表示模型狀態無效。
			if (ModelState.IsValid)
            {
                _context.Add(faq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

		// GET: Faqs/Edit/5
		// GET是透過網址列傳遞參數，通常用於獲取資料或顯示編輯表單，而POST則是用於提交修改後的資料到伺服器進行處理。
		// POST，當用戶提交表單時，瀏覽器會將表單數據封裝在HTTP請求的主體中，並通過HTTPS協議將請求發送到伺服器。伺服器接收到請求後，會根據URL中的id參數和表單數據中的faq對象來處理編輯操作。

		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

		// POST: Faqs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

		//Post方法的Edit(int id, [Bind("Id,Question,Answer,IsActive,DisplayOrder")] Faq faq)接受兩個參數：id和faq。id是從URL中傳遞過來的，而faq則是從表單數據中綁定過來的。


		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer,IsActive,DisplayOrder")] Faq faq)
        {

			//if (id != faq.Id)這行代碼的作用是檢查URL中的id是否與表單數據中的Id一致，如果不一致，則返回NotFound()，表示找不到該資源。這是一種安全措施，可以防止用戶通過修改URL或表單數據來嘗試修改其他資源。
			if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faq);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        // GET: Faqs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await _context.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

		// POST: Faqs/Delete/5
		// actionName("Delete")的作用是將DeleteConfirmed方法的路由名稱設置為Delete，這樣當用戶訪問Delete路由時，實際上會執行DeleteConfirmed方法。這是一種常見的做法，用於處理刪除操作的POST請求，因為通常我們希望在刪除之前先顯示一個確認頁面，讓用戶確認是否真的要刪除該資源。
		//避免有兩個Delete方法，分別處理GET和POST請求，這樣可以讓代碼更清晰，並且符合RESTful API的設計原則。
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq != null)
            {
                _context.Faqs.Remove(faq);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaqExists(int id)
        {
            return _context.Faqs.Any(e => e.Id == id);
        }
    }
}
