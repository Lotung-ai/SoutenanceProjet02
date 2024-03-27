using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {

        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines;

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private readonly List<CartLine> _cartLines = new List<CartLine>();
        /*private List<CartLine> GetCartLineList()
        {
            return new List<CartLine>();
        }*/

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            // DONE implement the method
            CartLine cartLine = _cartLines.FirstOrDefault(l => l.Product.Id == product.Id);
            if (cartLine == null)
            {
                cartLine = new CartLine
                {
                    OrderLineId = product.Id,
                    Product = product,
                    Quantity = quantity
                };
                _cartLines.Add(cartLine);
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            _cartLines.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // DONE implement the method
            return _cartLines.Sum(l => l.Product.Price * l.Quantity);

        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // DONE implement the method
            if (_cartLines.Count == 0)
                return 0.0;

            double totalValue = 0.0;
            int totalQuantity = 0;

            foreach (CartLine cartLine in _cartLines)
            {
                totalValue += cartLine.Product.Price * cartLine.Quantity;
                totalQuantity += cartLine.Quantity;
            }

              if (totalQuantity == 0)
                return 0.0;      

            return totalValue / totalQuantity;
            
           
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // DONE implement the method
            return _cartLines.FirstOrDefault(l => l.Product.Id == productId).Product; ;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = _cartLines;
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
