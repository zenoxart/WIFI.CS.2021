using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI.Anwendung
{
    /// <summary>
    /// Stellt einen Dienst zum
    /// Zurück- und Vorblättern
    /// in gesammelten Objekten bereit
    /// </summary>
    /// <remarks>Das Objekt ist so implementiert,
    /// dass das aktuelle Objekt im Zurückpuffer
    /// das oberste, das sichtbare, Element ist, 
    /// d.h. Zurückwechseln ist erst ab dem zweiten 
    /// hinzugefügten Objekt möglich</remarks>
    public class Verlauf
    {
        /// <summary>
        /// Wird ausgelöst, wenn im Verlauf
        /// nicht zurück gewechselt werden kann
        /// </summary>
        public event System.EventHandler KeinZurück;

        /// <summary>
        /// Löst das Ereignis KeinZurück aus
        /// </summary>
        protected virtual void OnKeinZurück()
        {
            var BehandlerKopie = this.KeinZurück;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn im Verlauf
        /// zurück gewechselt werden kann
        /// </summary>
        public event System.EventHandler ZurückMöglich;

        /// <summary>
        /// Löst das Ereignis ZurückMöglich aus
        /// </summary>
        protected virtual void OnZurückMöglich()
        {
            var BehandlerKopie = this.ZurückMöglich;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn im Verlauf
        /// nicht vorwärts gewechselt werden kann
        /// </summary>
        public event System.EventHandler KeinVorwärts;

        /// <summary>
        /// Löst das Ereignis KeinVorwärts aus
        /// </summary>
        protected virtual void OnKeinVorwärts()
        {
            var BehandlerKopie = this.KeinVorwärts;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn im Verlauf
        /// vorwärts gewechselt werden kann
        /// </summary>
        public event System.EventHandler VorwärtsMöglich;

        /// <summary>
        /// Löst das Ereignis VorwärtsMöglich aus
        /// </summary>
        protected virtual void OnVorwärtsMöglich()
        {
            var BehandlerKopie = this.VorwärtsMöglich;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.Collections.Stack _ZurückPuffer = null;

        /// <summary>
        /// Ruft die Liste mit den Objekten ab,
        /// zu denen zurückgewechselt werden kann
        /// </summary>
        protected System.Collections.Stack ZurückPuffer
        {
            get
            {
                if (this._ZurückPuffer == null)
                {
                    this._ZurückPuffer = new System.Collections.Stack();
                }

                return this._ZurückPuffer;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private System.Collections.Stack _VorwärtsPuffer = null;

        /// <summary>
        /// Ruft die Liste mit den Objekten ab,
        /// zu denen vorwärtsgewechselt werden kann
        /// </summary>
        protected System.Collections.Stack VorwärtsPuffer
        {
            get
            {
                if (this._VorwärtsPuffer == null)
                {
                    this._VorwärtsPuffer = new System.Collections.Stack();
                }

                return this._VorwärtsPuffer;
            }
        }

        /// <summary>
        /// Fügt dem Verlauf ein neues Objekt hinzu
        /// </summary>
        /// <param name="element">Objekt, das dem
        /// Verlauf hinzugefügt werden soll</param>
        /// <remarks>Anschließend ist kein Vorwärtswechseln möglich.
        /// Sollten sich mehr als ein Objekt im Verlauf befinden,
        /// wird ZurückMöglich ausgelöst.</remarks>
        public virtual void Hinterlegen(object element)
        {
            this.VorwärtsPuffer.Clear();
            this.OnKeinVorwärts();

            this.ZurückPuffer.Push(element);
            if (this.ZurückPuffer.Count > 1)
            {
                this.OnZurückMöglich();
            }

        }

        /// <summary>
        /// Stellt das oberste Element vom Zurückpuffer
        /// in den Vorwärtspuffer und gibt das oberste
        /// Element vom Zurückpuffer zurück.
        /// </summary>
        /// <returns>Ein Objekt, das früher hinterlegt wurde</returns>
        /// <remarks>Dabei wird immer VorwärtsMöglich ausgelöst.
        /// Sollte im Zurückpuffer nur mehr ein Objekt enthalten sein,
        /// wird KeinZurück ausgelöst</remarks>
        public virtual object HoleZurückObjekt()
        {
            this.VorwärtsPuffer.Push(this.ZurückPuffer.Pop());
            this.OnVorwärtsMöglich();

            if (this.ZurückPuffer.Count < 2)
            {
                this.OnKeinZurück();
            }

            return this.ZurückPuffer.Peek();
        }

        /// <summary>
        /// Stellt das oberste Element aus dem
        /// Vorwärtspuffer in den Zurückpuffer
        /// und gibt es zurück
        /// </summary>
        /// <returns>Ein Objekt, das früher im Verlauf
        /// hinterlegt wurde</returns>
        /// <remarks>Es wird KeinVorwärts ausgelöst,
        /// wenn dabei der Vorwärtpuffer geleert wurde.
        /// Sollten sich mehr als ein Objekt im
        /// Zurückpuffer befinden, wird ZurückMöglich ausgelöst</remarks>
        public virtual object HoleVorwärtsObjekt()
        {
            this.ZurückPuffer.Push(this.VorwärtsPuffer.Pop());

            if (this.VorwärtsPuffer.Count == 0)
            {
                this.OnKeinVorwärts();
            }

            if (this.ZurückPuffer.Count > 1)
            {
                this.OnZurückMöglich();
            }

            return this.ZurückPuffer.Peek();
        }
    }
}
