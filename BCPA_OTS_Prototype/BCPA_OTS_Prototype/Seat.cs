using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BCPA_OTS_Prototype
{
    public class Seat : SeatingLayout
    {
        /*====================================================================
         * Data
         * ==================================================================*/
        public enum         TicketTypes {  Child, Student, Adult, Senior };
        public enum         SeatStates  {  Available, Held, Booked };
        public SeatStates   seatStatus;  //Current state of seat
        public string       seatName;    //Name of seat, need not be unique
        public double       seatQuality; //1 = Best Seat, 0 = Worst Seat, set by layout
        public int
            seatID,                              //ID Number for seat, must be unique to the layout
            seatTimer,                           //Seat timer, countdown in seconds from 300 (5min) to 0, -1 when disabled
            seatPosX,                            //X position within picture box control
            seatPosY,                            //Y Position within picture box control
            seatPromoID,                         //Promo ID number from all available promotions, -1 if none
            controllingAgentID;                  //Controlling agent id from all controlling agents, -1 if none


        /*====================================================================
         * Constructor
         * ==================================================================*/
        public Seat(string name, int xPos, int yPos, SeatingLayout seatLayout, SeatStates status = SeatStates.Available, int promoID = -1, int agentID = -1)
        {
            //Set values from input
            seatName            = name;
            seatPosX            = xPos;
            seatPosY            = yPos;
            seatStatus          = status;
            seatPromoID         = promoID;
            controllingAgentID  = agentID;

            //Fil default values
            seatTimer = -1;

            //Create Seat ID using layout
            seatID = seatLayout.seatList.Count;
            
            //Add seat to seating layout list of seats
            seatLayout.seatList.Add(this);
        }

        /*-------------------------------------------------------------------
        * Get Seat Price
        *   Uses shows base seat price for given ticket type
        *   If no ticket type is given, adult is used by default
        *   ticket type enumerates are: Child, Student, Adult, Senior
        *   and then applies any show-wide discount to it,
        *   finally promotions are applied.
        * -----------------------------------------------------------------*/
        public double GetSeatPrice(TicketTypes selectedTicketType = TicketTypes.Adult)
        {
            //Returned result
            double finalPrice = 0;

            //Get seat base prices from the show, the parent of this objects parent, (our parent is seating layout, seating layout's parent is the show, which has the base prices)
            switch (selectedTicketType)
            {
                case TicketTypes.Child:
                    finalPrice = BasePriceChild;
                    break;
                case TicketTypes.Student:
                    finalPrice = BasePriceStudent;
                    break;
                case TicketTypes.Adult:
                    finalPrice = BasePriceAdult;
                    break;
                case TicketTypes.Senior:
                    finalPrice = BasePriceSenior;
                    break;
                default:
                    finalPrice = BasePriceAdult;
                    break;
            };

            //Use input of selected ticket type

            //Apply seat price to final

            //Check if there is any show-wide discount and apply it

            //Check if seat has any promotions applied to 

            //Apply any promotion

            //Return result
            return finalPrice;
        }
    }
}
