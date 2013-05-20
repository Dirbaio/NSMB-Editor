using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
	public partial class BehaviorEditor : UserControl {
		public BehaviorEditor() {
			InitializeComponent();

			Visible = false;

			// Set up stuff that won't change
			flagsListBox.Items.AddRange(
				LanguageManager.GetList("BehaviourFlags").ToArray());
			subTypeComboBox.Items.AddRange(
				LanguageManager.GetList("TileSubTypes").ToArray());
		}

		uint currentFlags;
		int currentSubType;
		int currentParam;

        public void setData(uint data)
        {
            setData(data, true);
        }

		private void setData(uint data, bool updateHex) {
			currentFlags = ((data & 0xFFFF0000) >> 16) | ((data & 0xF00) << 8);
			currentSubType = (int)(data & 0xF000) >> 12;
			currentParam = (int)data & 0xFF;

			DataUpdateFlag++;

			// populate the simple fields
			for (int i = 0; i < 20; i++) {
				flagsListBox.SetItemChecked(i, (currentFlags & (1 << i)) != 0);
			}

			subTypeComboBox.SelectedIndex = currentSubType;

			// set up the pipe/door stuff
			pipeDoorMainTypeComboBox.Items.AddRange(
				LanguageManager.GetList("DoorPipeMainSelection").ToArray());

			populateListBox(pipeDoorTypeListBox, 32, "PipeTileType");

			updateParamTypeAndData();
            if (updateHex)
                updateHexEditor();

			DataUpdateFlag--;

			Visible = true;
		}

        private void updateHexEditor()
        {
            hexBehaviorEditor.setArray(getBehaviorArray());
        }

		private void populateListBox(ListBox listBox, int maxID, string listName) {
			List<String> strings = LanguageManager.GetList(listName);

			listBox.BeginUpdate();
			listBox.Items.Clear();
			for (int i = 0; i < maxID; i++)
				listBox.Items.Add(string.Format("Unknown ({0} / 0x{0:X})", i));

			foreach (string item in strings) {
				int where = item.IndexOf('=');

				int idx;
				if (item.StartsWith("0x"))
					idx = int.Parse(item.Substring(2, where-2), System.Globalization.NumberStyles.AllowHexSpecifier);
				else
					idx = int.Parse(item.Substring(0, where));

				string text = item.Substring(where+1);

				listBox.Items[idx] = text;
			}
			listBox.EndUpdate();
		}

		enum ParamTypes {
			NullParam, CoinParam, QuestionParam, ExplodableParam,
			BrickParam, SlopeParam, PipeParam, ClimbParam,
			PartialBlockParam, HarmfulParam, DonutParam,
			ConveyorParam,
			DummyUnsetParam,
		}

		private ParamTypes effectiveParamType() {
			return effectiveParamType(currentFlags, currentSubType);
		}
		private ParamTypes effectiveParamType(uint flags, int subType) {
			List<ParamTypes> types = effectiveParamTypes(flags, subType);
			return types[0];
		}

		private List<ParamTypes> effectiveParamTypes(uint flags, int subType) {
			List<ParamTypes> output = new List<ParamTypes>();
			if ((flags & 2) != 0)
				output.Add(ParamTypes.CoinParam);
			if ((flags & 4) != 0)
				output.Add(ParamTypes.QuestionParam);
			if ((flags & 8) != 0)
				output.Add(ParamTypes.ExplodableParam);
			if ((flags & 0x10) != 0)
				output.Add(ParamTypes.BrickParam);
			if ((flags & (0x20 | 0x40)) != 0)
				output.Add(ParamTypes.SlopeParam);
			if ((flags & 0x100) != 0)
				output.Add(ParamTypes.PipeParam);
			if ((flags & 0x400) != 0)
				output.Add(ParamTypes.ClimbParam);
			if ((flags & 0x800) != 0)
				output.Add(ParamTypes.PartialBlockParam);
			if ((flags & 0x1000) != 0)
				output.Add(ParamTypes.HarmfulParam);
			if ((flags & 0x20000) != 0)
				output.Add(ParamTypes.DonutParam);
			if (subType == 4 || subType == 5)
				output.Add(ParamTypes.ConveyorParam);

			if (output.Count == 0)
				output.Add(ParamTypes.NullParam);

			return output;
		}

		private string getInternalLangNameForParamType(ParamTypes type) {
			switch (type) {
				case ParamTypes.NullParam: return "GenericTileParams";
				case ParamTypes.CoinParam: return "CoinParams";
				case ParamTypes.QuestionParam: return "QuestionParams";
				case ParamTypes.BrickParam: return "BrickParams";
				case ParamTypes.ExplodableParam: return "ExplodableParams";
				case ParamTypes.HarmfulParam: return "HarmfulTileParams";
				case ParamTypes.ConveyorParam: return "ConveyorParams";
				case ParamTypes.DonutParam: return "DonutParams";
				case ParamTypes.ClimbParam: return "FenceParams";
				case ParamTypes.SlopeParam: return "SlopeParams";
				case ParamTypes.PipeParam: return "PipeDoorParams";
				case ParamTypes.PartialBlockParam: return "PartialBlockParams";
			}

			return null;
		}

		private ParamTypes currentParamType = ParamTypes.DummyUnsetParam;

		private void updateParamTypeAndData() {
			List<ParamTypes> paramTypes = effectiveParamTypes(currentFlags, currentSubType);
			useParamType(paramTypes[0]);
			refreshParamData();

			if (paramTypes.Count == 1) {
				string typeName = LanguageManager.Get("BehaviorEditor",
					"type_" + getInternalLangNameForParamType(paramTypes[0]));

				paramsLabel.Text = string.Format(
					LanguageManager.Get("BehaviorEditor", "params_normal"),
					typeName);

			} else {
				string[] typeNames = new string[paramTypes.Count];
				for (int i = 0; i < paramTypes.Count; i++) {
					typeNames[i] = LanguageManager.Get("BehaviorEditor",
						"type_" + getInternalLangNameForParamType(paramTypes[i]));
				}

				paramsLabel.Text = string.Format(
					LanguageManager.Get("BehaviorEditor", "params_conflict"),
					string.Join(", ", typeNames));
			}
		}

		private void useParamType(ParamTypes type) {
			if (type != currentParamType) {
				DataUpdateFlag++;

				currentParamType = type;

				// Show the appropriate panel, and populate it appropriately
				paramsListBox.Visible = false;
				pipeDoorParamPanel.Visible = false;
				partialBlockParamPanel.Visible = false;

				if (type == ParamTypes.PipeParam) {
					pipeDoorParamPanel.Visible = true;

				} else if (type == ParamTypes.PartialBlockParam) {
					partialBlockParamPanel.Visible = true;

				} else {
					paramsListBox.Visible = true;

					populateListBox(paramsListBox, 0x100, getInternalLangNameForParamType(type));
				}

				DataUpdateFlag--;
			}
		}

		private void refreshParamData() {
			DataUpdateFlag++;

			if (currentParamType == ParamTypes.PipeParam) {
				pipeDoorMainTypeComboBox.SelectedIndex = currentParam >> 5;
				pipeDoorTypeListBox.SelectedIndex = currentParam & 0x1F;

			} else if (currentParamType == ParamTypes.PartialBlockParam) {
				topLeftPBCheckBox.Checked = ((currentParam & 1) != 0);
				topRightPBCheckBox.Checked = ((currentParam & 2) != 0);
				bottomLeftPBCheckBox.Checked = ((currentParam & 4) != 0);
				bottomRightPBCheckBox.Checked = ((currentParam & 8) != 0);

			} else {
				paramsListBox.SelectedIndex = currentParam;
			}

			DataUpdateFlag--;
		}

		private void sendDataChangedEvent() {
            if (DataUpdateFlag > 0) return;

            updateHexEditor();
            DataChanged(getBehaviorUInt());
		}

        private uint getBehaviorUInt()
        {
            return ((currentFlags & 0xFFFF) << 16) |
					((currentFlags & 0xF0000) >> 8) |
					(uint)(currentSubType << 12) |
					(uint)currentParam;
        }

        private byte[] getBehaviorArray()
        {
            return BitConverter.GetBytes(getBehaviorUInt());
        }

		int DataUpdateFlag = 0;

		public delegate void DataChangedDelegate(uint newData);
		public event DataChangedDelegate DataChanged;

		private void flagsListBox_ItemCheck(object sender, ItemCheckEventArgs e) {
			if (DataUpdateFlag > 0)
				return;

			uint mask = 1U << e.Index;
			if (e.NewValue == CheckState.Checked)
				currentFlags |= mask;
			else
				currentFlags &= ~mask;

			updateParamTypeAndData();
			sendDataChangedEvent();
		}

		private void subTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (DataUpdateFlag > 0)
				return;

			currentSubType = subTypeComboBox.SelectedIndex;
			updateParamTypeAndData();
			sendDataChangedEvent();
		}

		private void paramsListBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (DataUpdateFlag > 0)
				return;

			currentParam = paramsListBox.SelectedIndex;
			sendDataChangedEvent();
		}

		private void PBCheckBox_CheckedChanged(object sender, EventArgs e) {
			if (DataUpdateFlag > 0)
				return;

			currentParam =
				((topLeftPBCheckBox.Checked) ? 1 : 0) |
				((topRightPBCheckBox.Checked) ? 2 : 0) |
				((bottomLeftPBCheckBox.Checked) ? 4 : 0) |
				((bottomRightPBCheckBox.Checked) ? 8 : 0);
			sendDataChangedEvent();
		}

		private void pipeDoorMainTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (DataUpdateFlag > 0)
				return;

			currentParam &= 0x1F;
			currentParam |= (pipeDoorMainTypeComboBox.SelectedIndex << 5);
			sendDataChangedEvent();
		}

		private void pipeDoorTypeListBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (DataUpdateFlag > 0)
				return;

			currentParam &= 0xE0;
			currentParam |= pipeDoorTypeListBox.SelectedIndex;
			sendDataChangedEvent();
		}

        private void hexBehaviorEditor_ValueChanged(byte[] val)
        {
            if (DataUpdateFlag > 0) return;

            setData(BitConverter.ToUInt32(val, 0), false);
            // Doesn't use sendDataChangedEvent() because it will refresh the hexBehaviorEditor
            DataChanged(getBehaviorUInt());
        }

	}
}
