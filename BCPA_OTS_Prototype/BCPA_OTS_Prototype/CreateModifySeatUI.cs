using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPA_OTS_Prototype
{
    public partial class CreateModifySeatUI : Form
    {
        private Seat    seatObj;    //Seat we are creating / modifying
        private SeatUI  seatUIObj;     //Seat ui element

        public CreateModifySeatUI(Seat newSeat, SeatUI seatUI)
        {
            InitializeComponent();
            seatObj = newSeat;
            if (seatUI != null){ seatUIObj = seatUI; }
        }

        private void CreateModifySeatUI_Load(object sender, EventArgs e)
        {
            //Fill values with existing seat data
            tb_seatId.Text = seatObj.seatID.ToString();
            tb_seatName.Text = seatObj.seatName;
            tb_xPos.Text = seatObj.seatPosX.ToString();
            tb_yPos.Text = seatObj.seatPosY.ToString();
            cmb_seatStatus.Text = seatObj.seatStatus.ToString();
            cmb_seatAgent.SelectedIndex = seatObj.controllingAgentID + 1;
            cmb_seatPromo.SelectedIndex = seatObj.seatPromoID + 1;

            //Prices
            tb_adultPrice.Text = seatObj.GetSeatPrice(Seat.TicketTypes.Adult).ToString();
            tb_studentPrice.Text = seatObj.GetSeatPrice(Seat.TicketTypes.Student).ToString();
            tb_seniorPrice.Text = seatObj.GetSeatPrice(Seat.TicketTypes.Senior).ToString();
            tb_childPrice.Text = seatObj.GetSeatPrice(Seat.TicketTypes.Child).ToString();

        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            //Set seat values to ones in text boxes
            seatObj.seatName = tb_seatName.Text;
            seatObj.seatPosX = Convert.ToInt32(tb_xPos.Text);
            seatObj.seatPosY = Convert.ToInt32(tb_yPos.Text);
            seatUIObj.seatPicBox.Location = new Point(Convert.ToInt32(tb_xPos.Text), Convert.ToInt32(tb_yPos.Text));
            seatUIObj.Refresh();

            //Convert combo box to actual seat status type
            if        (cmb_seatStatus.Text.Contains("Available")){
                seatObj.seatStatus = Seat.SeatStates.Available;
            } else if (cmb_seatStatus.Text.Contains("Held")) {
                seatObj.seatStatus = Seat.SeatStates.Held;
            } else if (cmb_seatStatus.Text.Contains("Booked")) {
                seatObj.seatStatus = Seat.SeatStates.Booked;
            }

            //Take first two chars from combo box (eg -1 or 0: or 99 (max value is 99)), remove the :, and convert to int
            seatObj.controllingAgentID = Convert.ToInt32(cmb_seatAgent.Text.Substring(0, 2).TrimEnd(':'));

            //Take first two chars from combo box (eg -1 or 0: or 99 (max value is 99)), and convert them to int
            seatObj.seatPromoID = Convert.ToInt32(cmb_seatPromo.Text.Substring(0, 2).TrimEnd(':'));

            //Update seat colour
            if (seatUIObj != null) { seatUIObj.UpdateSeatColour(); };

            //Close the form
            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //Close the form
            Close();
        }
    }
}
