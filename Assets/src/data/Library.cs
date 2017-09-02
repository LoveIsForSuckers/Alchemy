using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class Library
{
    static private Library _instance;

    private ReagentLibrary _reagents;
    private ReactionLibrary _reactions;

    public Library(string jsonData)
    {
        _instance = this;

        var dataProxy = JsonConvert.DeserializeObject<LibraryDataProxy>(jsonData);
        _reagents = new ReagentLibrary(dataProxy.Reagents);
        _reactions = new ReactionLibrary(dataProxy.Reactions);
    }

    public ReagentLibrary Reagents { get { return _reagents; } }

    public ReactionLibrary Reactions { get { return _reactions; } }

    static public Library Instance { get { return _instance; } }
}

public class LibraryDataProxy
{
    public IEnumerable<ReagentLibItem> Reagents { get; set; }
    public IEnumerable<ReactionLibItem> Reactions { get; set; }
}
