using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Anno1800Calculator
{
    public partial class Form1 : Form
    {
		private Dictionary<NumericUpDown, decimal> timeForOne = new Dictionary<NumericUpDown, decimal>();
		private Dictionary<NumericUpDown, List<NumericUpDown>> neededResources = new Dictionary<NumericUpDown, List<NumericUpDown>>();
		private List<NumericUpDown> inputFields = new List<NumericUpDown>();
		private List<NumericUpDown> calculatedFields = new List<NumericUpDown>();

		public Form1()
        {
            this.InitializeComponent();

			this.timeForOne.Add(udBrillen			, 90);
			this.timeForOne.Add(udDynamitfabrik		, 60);
			this.timeForOne.Add(udEisenmine			, 15);
			this.timeForOne.Add(udFenster			, 60);
			this.timeForOne.Add(udGeschuetze		, 120);
			this.timeForOne.Add(udGlashuette		, 30);
			this.timeForOne.Add(udGluehbirnen		, 60);
			this.timeForOne.Add(udGluehfadenfabrik	, 60);
			this.timeForOne.Add(udGoldmine			, 150);
			this.timeForOne.Add(udGoldschmelze		, 60);
			this.timeForOne.Add(udGulaschkueche		, 120);
			this.timeForOne.Add(udHochofen			, 30);
			this.timeForOne.Add(udHochraeder		, 30);
			this.timeForOne.Add(udHolzfaeller		, 15);
			this.timeForOne.Add(udKanonen			, 90);
			this.timeForOne.Add(udKautschuk			, 60);
			this.timeForOne.Add(udKohlemine			, 15);
			this.timeForOne.Add(udKonserven			, 90);
			this.timeForOne.Add(udKunstschreinerei	, 60);
			this.timeForOne.Add(udKupfermine		, 30);
			this.timeForOne.Add(udMessinghuette		, 60);
			this.timeForOne.Add(udMotorenfabrik		, 90);
			this.timeForOne.Add(udNaehmaschinen		, 30);
			this.timeForOne.Add(udPaprika			, 120);
			this.timeForOne.Add(udPerlenfarm		, 90);
			this.timeForOne.Add(udPhonographen		, 120);
			this.timeForOne.Add(udQuarzgrube		, 30);
			this.timeForOne.Add(udRindfleisch		, 120);
			this.timeForOne.Add(udSalpeter			, 120);
			this.timeForOne.Add(udSchmuck			, 30);
			this.timeForOne.Add(udSchweinezucht		, 60);
			this.timeForOne.Add(udSekt				, 30);
			this.timeForOne.Add(udStahlbeton		, 60);
			this.timeForOne.Add(udStahltraeger		, 45);
			this.timeForOne.Add(udTaschenuhren		, 90);
			this.timeForOne.Add(udWasenmeisterei	, 60);
			this.timeForOne.Add(udWeinberg			, 120);
			this.timeForOne.Add(udZementmine		, 30);
			this.timeForOne.Add(udZinkmine			, 30);


			this.neededResources.Add(udBrillen			, new List<NumericUpDown> { udMessinghuette, udGlashuette });
			this.neededResources.Add(udDynamitfabrik	, new List<NumericUpDown> { udSalpeter, udWasenmeisterei});
			this.neededResources.Add(udFenster			, new List<NumericUpDown> { udGlashuette, udHolzfaeller });
			this.neededResources.Add(udGeschuetze		, new List<NumericUpDown> { udDynamitfabrik, udHochofen });
			this.neededResources.Add(udGlashuette		, new List<NumericUpDown> { udQuarzgrube });
			this.neededResources.Add(udGluehbirnen		, new List<NumericUpDown> { udGlashuette, udGluehfadenfabrik });
			this.neededResources.Add(udGluehfadenfabrik	, new List<NumericUpDown> { udKohlemine });
			this.neededResources.Add(udGoldschmelze		, new List<NumericUpDown> { udGoldmine, udKohlemine });
			this.neededResources.Add(udGulaschkueche	, new List<NumericUpDown> { udRindfleisch, udPaprika });
			this.neededResources.Add(udHochofen			, new List<NumericUpDown> { udEisenmine, udKohlemine });
			this.neededResources.Add(udHochraeder		, new List<NumericUpDown> { udHochofen, udKautschuk });
			this.neededResources.Add(udKanonen			, new List<NumericUpDown> { udHochofen });
			this.neededResources.Add(udKonserven		, new List<NumericUpDown> { udGulaschkueche, udEisenmine });
			this.neededResources.Add(udKunstschreinerei	, new List<NumericUpDown> { udHolzfaeller });
			this.neededResources.Add(udMessinghuette	, new List<NumericUpDown> { udZinkmine, udKupfermine });
			this.neededResources.Add(udMotorenfabrik	, new List<NumericUpDown> { udHochofen, udMessinghuette });
			this.neededResources.Add(udNaehmaschinen	, new List<NumericUpDown> { udHochofen, udHolzfaeller });
			this.neededResources.Add(udPhonographen		, new List<NumericUpDown> { udKunstschreinerei, udMessinghuette });
			this.neededResources.Add(udSchmuck			, new List<NumericUpDown> { udGoldschmelze, udPerlenfarm });
			this.neededResources.Add(udSekt				, new List<NumericUpDown> { udGlashuette, udWeinberg });
			this.neededResources.Add(udStahlbeton		, new List<NumericUpDown> { udHochofen, udZementmine });
			this.neededResources.Add(udStahltraeger		, new List<NumericUpDown> { udHochofen });
			this.neededResources.Add(udTaschenuhren		, new List<NumericUpDown> { udGoldschmelze, udGlashuette });
			this.neededResources.Add(udWasenmeisterei	, new List<NumericUpDown> { udSchweinezucht });

			foreach (var group in this.Controls.OfType<GroupBox>())
			{
				foreach (var control in group.Controls.OfType<NumericUpDown>())
				{
					if (!control.ReadOnly)
					{
						this.inputFields.Add(control);
						control.ValueChanged += this.NumericUpDown_ValueChanged;
					}
					else
					{
						this.calculatedFields.Add(control);
					}
				}
			}
		}

		private void NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			this.RecalcAll();
		}

		private void RecalcAll()
		{
			foreach (var control in calculatedFields)
			{
				control.Value = 0;
			}

			foreach (var inputField in this.inputFields)
			{
				this.RecalcDependency(inputField, inputField.Value);
			}	
		}

		private void RecalcDependency(NumericUpDown changedField, decimal addedValue)
		{
			if (addedValue > 0)
			{
				this.neededResources.TryGetValue(changedField, out List<NumericUpDown> dependendFields);
				if (dependendFields != null)
				{
					foreach (var dependency in dependendFields)
					{
						var fieldTime = this.timeForOne[changedField];
						var dependencyTime = this.timeForOne[dependency];
						var ratio = dependencyTime / fieldTime;
						var neededDependencyRessourceForThisField = addedValue * ratio;

						dependency.Value += neededDependencyRessourceForThisField;

						this.RecalcDependency(dependency, neededDependencyRessourceForThisField);
					}
				}
			}
		}
	}
}
