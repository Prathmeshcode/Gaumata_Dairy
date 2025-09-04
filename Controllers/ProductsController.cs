using Microsoft.AspNetCore.Mvc;
using GaumataWeb.Models;
using Newtonsoft.Json;

namespace GaumataWeb.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly List<Product> products = new()
        {
            new Product {
                Id = 1,
                Name = "Fresh Cow Milk",
                MarathiName = "ताजे गाईचे दूध",
                Description = "Pure and organic cow milk collected fresh daily from our farm.",
                MarathiDescription = "आमच्या शेतातून दररोज गोळा केलेले शुद्ध आणि सेंद्रिय गाईचे दूध.",
                Price = 50,
                Unit = "₹50 / लिटर",
                ImageUrl = "/img/m2.jpg",
                Features = new List<string>{"1 लिटर बाटली", "२४ तासांची ताजेपणा", "हार्मोन्स नाही"},
                MarathiFeatures = new List<string>{"१ लिटर व्हॉल्यूम", "२४ तासांची ताजेपणा हमी", "हार्मोन्स मुक्त"}
            },
            new Product {
                Id = 2,
                Name = "Fresh Paneer",
                MarathiName = "ताजा पनीर",
                Description = "Soft and fresh paneer made from pure milk.",
                MarathiDescription = "शुद्ध दुधापासून बनवलेला मऊ आणि ताजा पनीर.",
                Price = 150,
                Unit = "₹150 / 200g",
                ImageUrl = "/img/paneer.webp",
                Features = new List<string>{"२०० ग्रॅम वजन", "मिश्रण नाही", "घरगुती चव"},
                MarathiFeatures = new List<string>{"२०० ग्रॅम वजन", "कोणतेही मिश्रण नाही", "घरगुती स्वाद"}
            },
            new Product {
                Id = 3,
                Name = "Fresh Curd",
                MarathiName = "ताजे दही",
                Description = "Thick and creamy curd made using traditional methods.",
                MarathiDescription = "पारंपरिक पद्धतीने तयार केलेले घट्ट आणि मलाईदार दही.",
                Price = 60,
                Unit = "₹60 / 500g",
                ImageUrl = "/img/curd.webp",
                Features = new List<string>{"५०० ग्रॅम व्हॉल्यूम", "प्रोबायोटिक्स भरपूर", "परिरक्षके नाही"},
                MarathiFeatures = new List<string>{"५०० ग्रॅम व्हॉल्यूम", "प्रोबायोटिक्स समृद्ध", "कोणतेही परिरक्षके नाही"}
            },
            new Product {
                Id = 4,
                Name = "Fresh Butter",
                MarathiName = "ताजे लोणी",
                Description = "Creamy fresh butter churned from pure milk.",
                MarathiDescription = "शुद्ध दुधापासून मंथन करून तयार केलेले मलाईदार ताजे लोणी.",
                Price = 180,
                Unit = "₹180 / 250g",
                ImageUrl = "/img/loni.jpg",
                Features = new List<string>{"२५० ग्रॅम वजन", "स्वयंपाक आणि बेकिंगसाठी योग्य", "मऊ पोत"},
                MarathiFeatures = new List<string>{"२५० ग्रॅम वजन", "स्वयंपाक आणि बेकिंगसाठी आदर्श", "गुळगुळीत पोत"}
            },
            new Product {
                Id = 5,
                Name = "Pure Ghee",
                MarathiName = "शुद्ध तूप",
                Description = "Traditional desi ghee made with rich aroma and flavor.",
                MarathiDescription = "समृद्ध सुगंध आणि चवीसह बनवलेले पारंपरिक देशी तूप.",
                Price = 450,
                Unit = "₹450 / 500ml",
                ImageUrl = "/img/g3.jpg",
                Features = new List<string>{"५०० मिली व्हॉल्यूम", "पचनशक्ती वाढवते", "दीर्घकाळ टिकणारे"},
                MarathiFeatures = new List<string>{"५०० मिली प्रमाण", "पचनक्रिया सुधारते", "दीर्घ शेल्फ लाइफ"}
            },
            new Product {
                Id = 6,
                Name = "Flavored Milk",
                MarathiName = "चॉकलेट दूध",
                Description = "Delicious chocolate flavored milk enriched with nutrients.",
                MarathiDescription = "पोषक तत्वांनी भरपूर चविष्ट चॉकलेट फ्लेवर दूध.",
                Price = 40,
                Unit = "₹40 / पॅक",
                ImageUrl = "/img/c2.jpg",
                Features = new List<string>{"३०० मिली व्हॉल्यूम", "कुटुंबाचे आवडते", "परिरक्षके नाही"},
                MarathiFeatures = new List<string>{"३०० मिली प्रमाण", "कुटुंबाचे प्रिय", "कोणतेही परिरक्षके नाही"}
            }
        };

        public IActionResult Index()
        {
            return View(products);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return Json(new { success = false, message = "उत्पादन सापडले नाही" });

            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(c => c.Product.Id == id);
            if (existingItem != null)
                existingItem.Quantity += quantity;
            else
                cart.Add(new CartItem { Product = product, Quantity = quantity });

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new
            {
                success = true,
                message = "उत्पादन कार्टमध्ये जोडले गेले!",
                totalItems = cart.Sum(c => c.Quantity),
                totalAmount = cart.Sum(c => c.TotalPrice)
            });
        }

        public IActionResult Cart()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.RemoveAll(c => c.Product.Id == id);
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return Json(new { success = true, message = "उत्पादन काढले गेले!" });
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "कृपया सर्व माहिती भरा" });
            }

            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (!cart.Any())
            {
                return Json(new { success = false, message = "कार्ट रिकामे आहे" });
            }

            order.Items = cart;
            order.TotalAmount = cart.Sum(c => c.TotalPrice);
            order.OrderDate = DateTime.Now;

            // Save to database here

            // Clear cart
            HttpContext.Session.Remove("Cart");

            return Json(new
            {
                success = true,
                message = "तुमचा ऑर्डर यशस्वीपणे पाठविला गेला! आम्ही लवकरच संपर्क करू.",
                orderId = order.Id
            });
        }
    }
}

public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}
