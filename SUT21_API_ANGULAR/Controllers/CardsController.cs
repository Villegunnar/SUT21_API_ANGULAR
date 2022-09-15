using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SUT21_API_ANGULAR.Models;
using System.Collections.Specialized;

namespace SUT21_API_ANGULAR.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {

        private readonly CardsDbContext _cardContext;

        public CardsController(CardsDbContext cardContext)
        {
            _cardContext = cardContext;
        }

        // Get All Cards

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {

             var cards = await _cardContext.Cards.ToListAsync();

            return Ok(cards);
        }

        // Get Single Card

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetSingleCard")]
        public async Task<IActionResult> GetSingleCard([FromRoute]Guid id)
        {
            var card =  await _cardContext.Cards.FirstOrDefaultAsync(c => c.Id == id);


            if (card != null)
            {
                return Ok(card);
            }
            return NotFound("Card Not Found");
        }

        //Add Card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody]Card card)
        {
            card.Id = Guid.NewGuid();
            await _cardContext.Cards.AddAsync(card);
            await _cardContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingleCard), new {id = card.Id},card);

        }

        // Update Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateCard([FromRoute]Guid id,Card card)
        {
            var cardToUpdate = await _cardContext.Cards.FirstOrDefaultAsync(c => c.Id == id);

            if (cardToUpdate != null)
            {
                cardToUpdate.HolderName = card.HolderName;
                cardToUpdate.CardNumber = card.CardNumber;
                cardToUpdate.ExpirMonth = card.ExpirMonth;
                cardToUpdate.ExpirYear = card.ExpirYear;
                cardToUpdate.CVC = card.CVC;

                await _cardContext.SaveChangesAsync();

                return Ok(cardToUpdate);
            }
            return NotFound("Card Not Found To Update");
        }

        // Delete Card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteCard([FromRoute]Guid id)
        {
            var cardToDelete = await _cardContext.Cards.FirstOrDefaultAsync(c => c.Id == id);

            if (cardToDelete != null)
            {

                _cardContext.Cards.Remove(cardToDelete);
                await _cardContext.SaveChangesAsync();

                return Ok(cardToDelete);

            }

            return NotFound("Card Was Not Found To Delete");
        }

    }
}
