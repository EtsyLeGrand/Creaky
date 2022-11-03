/// <summary>
/// Un objet inspectable est un objet avec l'interaction "Inspect".
/// Une fois qu'un objet est inspecté, le message d'inspection est lu par le DialogueManager.
/// Une interaction peut être vide (que du texte), mais elle peut aussi avoir des propriétés, 
/// comme être instantanée (inst) ou se terminer par un choix (y/n).
/// </summary>
public interface IInspectable
{
    public string PropName
    {
        get;
    }

    public string[] InspectKeys
    {
        get;
    }

    public void Inspect();
}
