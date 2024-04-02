using FluentAssertions;

namespace KaydenMiller.BattleTech.Helper.Cli.Test.Unit;

public class BattleTechHtmlParserTests
{
    [Fact]
    public void Should_ParseInfoboxAsSolarSystem_WhenGivenValidInfobox()
    {
        // Arrange
        var infobox = new Infobox("""
                                  <table class="infobox"><tbody><tr><th colspan="2" class="infobox-above" style="background:#c5d07d; border:0.15em solid #222; padding:0.2em;">Wynn's Roost</th></tr><tr><td colspan="2" class="infobox-subheader" style="border:0.15em solid #222; padding:0.2em;"><div class="infoboxnavlink"><div class="system-nav-left">← 3135</div> <div class="system-nav-current">3151</div> <div class="system-nav-right"></div></div></td></tr><tr><td colspan="2" class="infobox-image" style="border:0.15em solid #222; padding:0.2em;"><div class="center"><div class="floatnone"><a href="/wiki/File:Wynn%27s_Roost_3151.svg" class="image"><img alt="Wynn's Roost 3151.svg" src="https://cfw.sarna.net/wiki/images/9/95/Wynn%27s_Roost_3151.svg?timestamp=20210827191047" width="240" height="274"></a></div></div><div class="infobox-caption">Wynn's Roost <a href="#Nearby_Systems">nearby systems</a><br>(<a href="/wiki/BattleTechWiki:Map_Legend" title="BattleTechWiki:Map Legend">Map Legend</a>)</div></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">System Information</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">X:Y Coordinates</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">610.609&nbsp;: 269.932<sup class="noprint" style="white-space:nowrap">[<i><a href="/wiki/BattleTechWiki:System_coordinates" title="BattleTechWiki:System coordinates">e</a></i>]</sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Spectral class</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">K7V<sup id="cite_ref-TtSWRp4_1-0" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Recharge time</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">198 hours<sup id="cite_ref-TtSWRp4_1-1" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Recharge station(s)</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">None<sup id="cite_ref-TtSWRp4_1-2" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Planet(s)</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">8<sup id="cite_ref-TtSWRp4_1-3" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr></tbody></table>
                                  """,
            "");

        // Act
        var metadata = BattleTechHtmlParser.ParseInfoBox(infobox);

        // Assert
        metadata.Should().ContainKeys([
            "X:Y Coordinates",
            "Spectral class",
            "Recharge time",
            "Recharge station(s)",
            "Planet(s)"
        ]);
        metadata.Should().ContainValues([
            "610.609 : 269.932[e]",
            "K7V[1]",
            "198 hours[1]",
            "None[1]",
            "8[1]"
        ]);
    }

    [Fact]
    public void Should_ParseInfoboxAsPlanet_WhenGivenValidPlanetInfobox()
    {
        var infobox = new Infobox("""
            <table class="infobox"><tbody><tr><th colspan="2" class="infobox-above" style="background:#c5d07d; border:0.15em solid #222; padding:0.2em;">Wynn's Roost II</th></tr><tr><td colspan="2" class="infobox-image" style="border:0.15em solid #222; padding:0.2em;"><div class="center"><div class="floatnone"><a href="/wiki/File:Wynns_Roost_Orbital_View.jpg" class="image"><img alt="Wynns Roost Orbital View.jpg" src="https://cfw.sarna.net/wiki/images/thumb/4/47/Wynns_Roost_Orbital_View.jpg/210px-or5zkkvgsy0n6rm4risfvi3genyel4q.jpg?timestamp=20240124011104" decoding="async" loading="lazy" width="210" height="182" srcset="https://cfw.sarna.net/wiki/images/thumb/4/47/Wynns_Roost_Orbital_View.jpg/315px-or5zkkvgsy0n6rm4risfvi3genyel4q.jpg?timestamp=20240124011104 1.5x, https://cfw.sarna.net/wiki/images/thumb/4/47/Wynns_Roost_Orbital_View.jpg/420px-or5zkkvgsy0n6rm4risfvi3genyel4q.jpg?timestamp=20240124011104 2x"></a></div></div></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">Astrophysical</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">System position</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Second<sup id="cite_ref-TtSWRp4_1-5" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Jump Point distance</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">3.70 days<sup id="cite_ref-TtSWRp4_1-6" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Moons</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">1 (Santa Monica)<sup id="cite_ref-TtSWRp4_1-7" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">Geophysical</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Surface gravity</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">1.0<sup id="cite_ref-TtSWRp4_1-8" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Atmospheric pressure</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Standard (Tainted)<sup id="cite_ref-TtSWRp4_1-9" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Equatorial temperature</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">34°C (Temperate)<sup id="cite_ref-TtSWRp4_1-10" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Surface water</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">50%<sup id="cite_ref-TtSWRp4_1-11" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Highest native life</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Amphibian<sup id="cite_ref-TtSWRp4_1-12" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Landmasses</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">5 (Turnerland, Wynnland, Oneland, Twoland, Threeland)<sup id="cite_ref-TtS:WRp11_34-0" class="reference"><a href="#cite_note-TtS:WRp11-34">[34]</a></sup></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">History and Culture</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Population</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">92,700,000 (3130)<sup id="cite_ref-TtSWRp4_1-13" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">Government and Infrastructure</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Political Leader</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;"><a href="/wiki/President_of_Wynn%27s_Roost" title="President of Wynn's Roost">President</a></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Capital</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Turnerville</td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">HPG Class</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">B (2787)<sup id="cite_ref-TtSWRp4_1-14" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup><br> none (3130)<sup id="cite_ref-TtSWRp4_1-15" class="reference"><a href="#cite_note-TtSWRp4-1">[1]</a></sup></td></tr></tbody></table>
            """,
            ""
        );

        var metadata = BattleTechHtmlParser.ParseInfoBox(infobox);

        metadata.Should().ContainKeys([
            "System position",
            "Jump Point distance",
            "Moons",
            "Surface gravity",
            "Atmospheric pressure",
            "Equatorial temperature",
            "Surface water",
            "Highest native life",
            "Landmasses",
            "Population",
            "Political Leader",
            "Capital",
            "HPG Class"
        ]);
    }

    /// <summary>
    /// https://www.sarna.net/wiki/Hibuarius
    /// </summary>
    [Fact]
    public void Should_ParseSystemHibuarius()
    {
        var page = """
            <div id="bodyContent" style="margin-right: 310px">
                    <div>
                        <div id="contentSub"></div>
                                <!-- start content -->
                <div id="mw-content-text" lang="en" dir="ltr" class="mw-content-ltr"><div class="mw-notification-area mw-notification-area-layout" id="mw-notification-area" style="display: none;"></div><div class="mw-parser-output"><style data-mw-deduplicate="TemplateStyles:r1004727">.mw-parser-output .infobox-subbox{padding:0;border:none;margin:-3px;width:auto;min-width:100%;font-size:100%;clear:none;float:none;background-color:transparent}.mw-parser-output .infobox-3cols-child{margin:auto}.mw-parser-output .infobox .navbar{font-size:100%}body.skin-minerva .mw-parser-output .infobox-header,body.skin-minerva .mw-parser-output .infobox-subheader,body.skin-minerva .mw-parser-output .infobox-above,body.skin-minerva .mw-parser-output .infobox-title,body.skin-minerva .mw-parser-output .infobox-image,body.skin-minerva .mw-parser-output .infobox-full-data,body.skin-minerva .mw-parser-output .infobox-below{text-align:center}</style><table class="infobox"><tbody><tr><th colspan="2" class="infobox-above" style="background:#c5d07d; border:0.15em solid #222; padding:0.2em;">Hibuarius</th></tr><tr><td colspan="2" class="infobox-subheader" style="border:0.15em solid #222; padding:0.2em;"><div class="infoboxnavlink"><div class="system-nav-left">← 3135</div> <div class="system-nav-current">3151</div> <div class="system-nav-right"></div></div></td></tr><tr><td colspan="2" class="infobox-image" style="border:0.15em solid #222; padding:0.2em;"><div class="center"><div class="floatnone"><a href="/wiki/File:Hibuarius_3151.svg" class="image"><img alt="Hibuarius 3151.svg" src="https://cfw.sarna.net/wiki/images/f/fa/Hibuarius_3151.svg?timestamp=20210827184359" width="240" height="274"></a></div></div><div class="infobox-caption">Hibuarius <a href="#Nearby_Systems">nearby systems</a><br>(<a href="/wiki/BattleTechWiki:Map_Legend" title="BattleTechWiki:Map Legend">Map Legend</a>)</div></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">System Information</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">X:Y Coordinates</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">-38.682&nbsp;: -410.743<sup class="noprint" style="white-space:nowrap">[<i><a href="/wiki/BattleTechWiki:System_coordinates" title="BattleTechWiki:System coordinates">e</a></i>]</sup></td></tr></tbody></table>
            <div id="toc" class="toc"><input type="checkbox" role="button" id="toctogglecheckbox" class="toctogglecheckbox" style="display:none"><div class="toctitle" lang="en" dir="ltr"><h2>Contents</h2><span class="toctogglespan"><label class="toctogglelabel" for="toctogglecheckbox"></label></span></div>
            <ul>
            <li class="toclevel-1 tocsection-1"><a href="#Political_Affiliation"><span class="tocnumber">1</span> <span class="toctext">Political Affiliation</span></a></li>
            <li class="toclevel-1 tocsection-2"><a href="#Planetary_Info"><span class="tocnumber">2</span> <span class="toctext">Planetary Info</span></a></li>
            <li class="toclevel-1 tocsection-3"><a href="#Map_Gallery"><span class="tocnumber">3</span> <span class="toctext">Map Gallery</span></a></li>
            <li class="toclevel-1 tocsection-4"><a href="#Nearby_Systems"><span class="tocnumber">4</span> <span class="toctext">Nearby Systems</span></a></li>
            <li class="toclevel-1 tocsection-5"><a href="#References"><span class="tocnumber">5</span> <span class="toctext">References</span></a></li>
            <li class="toclevel-1 tocsection-6"><a href="#Bibliography"><span class="tocnumber">6</span> <span class="toctext">Bibliography</span></a></li>
            </ul>
            </div>
            
            <div class="sarna_inline_mobile_ad"><div id="nn_mobile_mpu1"></div><div align="center" data-freestar-ad="__300x250" id="sarna_mobile_incontent_1"><script data-cfasync="false" type="text/javascript">if(window.freestar){freestar.config.enabled_slots.push({ placementName: 'sarna_mobile_incontent_1', slotId: 'sarna_mobile_incontent_1' });}</script></div></div><h2><span class="mw-headline" id="Political_Affiliation">Political Affiliation</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Hibuarius&amp;action=edit&amp;section=1" title="Edit section: Political Affiliation">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <style data-mw-deduplicate="TemplateStyles:r983948">.mw-parser-output .div-col{margin-top:0.3em;column-width:30em}.mw-parser-output .div-col-small{font-size:90%}.mw-parser-output .div-col-rules{column-rule:1px solid #aaa}.mw-parser-output .div-col dl,.mw-parser-output .div-col ol,.mw-parser-output .div-col ul{margin-top:0}.mw-parser-output .div-col li,.mw-parser-output .div-col dd{page-break-inside:avoid;break-inside:avoid-column}.mw-parser-output .plainlist ol,.mw-parser-output .plainlist ul{line-height:inherit;list-style:none;margin:0}.mw-parser-output .plainlist ol li,.mw-parser-output .plainlist ul li{margin-bottom:0}</style><div class="div-col" style="column-width: 25em;">
            <ul><li><a href="/wiki/2366" title="2366">2366</a> - No record <sup id="cite_ref-HB:HLp17_1-0" class="reference"><a href="#cite_note-HB:HLp17-1">[1]</a></sup></li>
            <li><a href="/wiki/2571" title="2571">2571</a> - No record <sup id="cite_ref-HB:HLp25_2-0" class="reference"><a href="#cite_note-HB:HLp25-2">[2]</a></sup></li>
            <li><a href="/wiki/2596" title="2596">2596</a> - No record <sup id="cite_ref-H:RWp159_3-0" class="reference"><a href="#cite_note-H:RWp159-3">[3]</a></sup></li>
            <li><a href="/wiki/2750" title="2750">2750</a> - <a href="/wiki/Capellan_Confederation" title="Capellan Confederation">Capellan Confederation</a> <sup id="cite_ref-ER:2750p37_4-0" class="reference"><a href="#cite_note-ER:2750p37-4">[4]</a></sup></li>
            <li><a href="/wiki/2764" title="2764">2764</a> - Capellan Confederation <sup id="cite_ref-FM:SLDFpvii_5-0" class="reference"><a href="#cite_note-FM:SLDFpvii-5">[5]</a></sup></li>
            <li><a href="/wiki/2765" title="2765">2765</a> - Capellan Confederation <sup id="cite_ref-H:LoTv1p11_6-0" class="reference"><a href="#cite_note-H:LoTv1p11-6">[6]</a></sup></li>
            <li><a href="/wiki/2786" title="2786">2786</a> - Capellan Confederation <sup id="cite_ref-FSW.28SB.29p24-25_7-0" class="reference"><a href="#cite_note-FSW.28SB.29p24-25-7">[7]</a></sup></li>
            <li><a href="/wiki/2822" title="2822">2822</a> - Capellan Confederation <sup id="cite_ref-HB:HLp31_8-0" class="reference"><a href="#cite_note-HB:HLp31-8">[8]</a></sup><sup id="cite_ref-H:LoTV2p122_9-0" class="reference"><a href="#cite_note-H:LoTV2p122-9">[9]</a></sup><sup id="cite_ref-FSW.28SB.29p112-113_10-0" class="reference"><a href="#cite_note-FSW.28SB.29p112-113-10">[10]</a></sup></li>
            <li><a href="/wiki/2864" title="2864">2864</a> - Capellan Confederation <sup id="cite_ref-HB:HLp39_11-0" class="reference"><a href="#cite_note-HB:HLp39-11">[11]</a></sup></li>
            <li><a href="/wiki/2890" title="2890">2890</a> - Capellan Confederation <sup id="cite_ref-HAtACp12_12-0" class="reference"><a href="#cite_note-HAtACp12-12">[12]</a></sup></li>
            <li><a href="/wiki/3022" title="3022">3022</a> - Independent world <sup id="cite_ref-HAtACp1s4-15_13-0" class="reference"><a href="#cite_note-HAtACp1s4-15-13">[13]</a></sup></li>
            <li><a href="/wiki/3025" title="3025">3025</a> - No record <sup id="cite_ref-HB:HLp40_14-0" class="reference"><a href="#cite_note-HB:HLp40-14">[14]</a></sup></li>
            <li><a href="/wiki/3030" title="3030">3030</a> - No record <sup id="cite_ref-HB:HLp49_15-0" class="reference"><a href="#cite_note-HB:HLp49-15">[15]</a></sup></li>
            <li><a href="/wiki/3040" title="3040">3040</a> - No record <sup id="cite_ref-H:Wo3039p132_16-0" class="reference"><a href="#cite_note-H:Wo3039p132-16">[16]</a></sup></li>
            <li><a href="/wiki/3050" title="3050">3050</a> - No record <sup id="cite_ref-ER:3052p10_17-0" class="reference"><a href="#cite_note-ER:3052p10-17">[17]</a></sup></li>
            <li><a href="/wiki/3052" title="3052">3052</a> - No record <sup id="cite_ref-ER:3052p22_18-0" class="reference"><a href="#cite_note-ER:3052p22-18">[18]</a></sup></li>
            <li><a href="/wiki/3057" title="3057">3057</a> - No record <sup id="cite_ref-ER:3062p10_19-0" class="reference"><a href="#cite_note-ER:3062p10-19">[19]</a></sup></li>
            <li><a href="/wiki/3058" title="3058">3058</a> - No record <sup id="cite_ref-HB:HLp60_20-0" class="reference"><a href="#cite_note-HB:HLp60-20">[20]</a></sup></li>
            <li><a href="/wiki/3063" title="3063">3063</a> - No record <sup id="cite_ref-ER:3062p29_21-0" class="reference"><a href="#cite_note-ER:3062p29-21">[21]</a></sup></li>
            <li><a href="/wiki/3067" title="3067">3067</a> - No record <sup id="cite_ref-HB:HLp68_22-0" class="reference"><a href="#cite_note-HB:HLp68-22">[22]</a></sup><sup id="cite_ref-J:FRp43_23-0" class="reference"><a href="#cite_note-J:FRp43-23">[23]</a></sup></li>
            <li><a href="/wiki/3075" title="3075">3075</a> - No record <sup id="cite_ref-JS:TBDp64_24-0" class="reference"><a href="#cite_note-JS:TBDp64-24">[24]</a></sup></li>
            <li><a href="/wiki/3079" title="3079">3079</a> - No record <sup id="cite_ref-FR:CCAFp21_25-0" class="reference"><a href="#cite_note-FR:CCAFp21-25">[25]</a></sup></li>
            <li><a href="/wiki/3081" title="3081">3081</a> - No record <sup id="cite_ref-J:FRp63_26-0" class="reference"><a href="#cite_note-J:FRp63-26">[26]</a></sup></li>
            <li><a href="/wiki/3085" title="3085">3085</a> - No record <sup id="cite_ref-FM:3085pvii_27-0" class="reference"><a href="#cite_note-FM:3085pvii-27">[27]</a></sup></li>
            <li><a href="/wiki/3135" title="3135">3135</a> - No record <sup id="cite_ref-ER:3145p10_28-0" class="reference"><a href="#cite_note-ER:3145p10-28">[28]</a></sup></li>
            <li><a href="/wiki/3145" title="3145">3145</a> - No record <sup id="cite_ref-ER:3145p38_29-0" class="reference"><a href="#cite_note-ER:3145p38-29">[29]</a></sup><sup id="cite_ref-FM:3145pVI_30-0" class="reference"><a href="#cite_note-FM:3145pVI-30">[30]</a></sup></li></ul>
            </div>
            <hr>
            <h2><span class="mw-headline" id="Planetary_Info">Planetary Info</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Hibuarius&amp;action=edit&amp;section=2" title="Edit section: Planetary Info">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <p><b>Hibuarius</b> was a part of the <a href="/wiki/Sian_Commonality" title="Sian Commonality">Sian Commonality</a> of the Capellan Confederation, located on the border with the <a href="/wiki/Periphery" title="Periphery">Periphery</a> near the worlds of <a href="/wiki/Arn" class="mw-redirect" title="Arn">Arn</a> and <a href="/wiki/Segerica" title="Segerica">Segerica</a>, and vanished from maps during the <a href="/wiki/3rd_Succession_War" class="mw-redirect" title="3rd Succession War">3rd Succession War</a>.<sup id="cite_ref-HB:HLp39_11-1" class="reference"><a href="#cite_note-HB:HLp39-11">[11]</a></sup><sup id="cite_ref-HB:HLp40_14-1" class="reference"><a href="#cite_note-HB:HLp40-14">[14]</a></sup>
            </p>
            <div style="text-align: center; background:#ffffcc; border: 1px solid #A2A200; padding: 0.5em 1em; margin: 0.5em auto; overflow: hidden">
            <p><b>Apocryphal Content Starts</b>
            </p>
            <p style="font-size: 80%;">The information after this notice comes from apocryphal sources; the <a href="/wiki/Canon" title="Canon">canonicity</a> of such information is uncertain.<br> Please view the reference page for information regarding their canonicity.</p>
            </div>
            <p>The star map in the apocryphal <a href="/wiki/BattleTech_(Video_Game)" title="BattleTech (Video Game)">BattleTech (Video Game)</a>, set in <a href="/wiki/3025" title="3025">3025</a>, states that Hibuarius is ruled by a theocratic government. The planet is described as a lush forest world where most of the native animal and insect species are extremely toxic to human life.<sup id="cite_ref-BTVG_31-0" class="reference"><a href="#cite_note-BTVG-31">[31]</a></sup>
            </p>
            <div style="text-align: center; background:#ffffcc; border: 1px solid #A2A200; padding: 0.5em 1em; margin: 0.5em auto; overflow: hidden">
            <p><b>Apocryphal Content Ends</b>
            </p>
            </div>
            <div class="sarna_inline_mobile_ad"><div id="nn_mobile_mpu2"></div><div align="center" data-freestar-ad="__300x250" id="sarna_mobile_incontent_2"><script data-cfasync="false" type="text/javascript">if(window.freestar){freestar.config.enabled_slots.push({ placementName: 'sarna_mobile_incontent_2', slotId: 'sarna_mobile_incontent_2' });}</script></div></div><h2><span class="mw-headline" id="Map_Gallery">Map Gallery</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Hibuarius&amp;action=edit&amp;section=3" title="Edit section: Map Gallery">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <div class="system-map-gallery"><div class="system-map-gallery-years"><div class="system-map-gallery-year-header">Years:</div><div class="system-map-gallery-year" data-year="2571">2571</div><div class="system-map-gallery-year" data-year="2596">2596</div><div class="system-map-gallery-year" data-year="2750">2750</div><div class="system-map-gallery-year" data-year="2765">2765</div><div class="system-map-gallery-year" data-year="2767">2767</div><div class="system-map-gallery-year" data-year="2783">2783</div><div class="system-map-gallery-year" data-year="2786">2786</div><div class="system-map-gallery-year" data-year="2821">2821</div><div class="system-map-gallery-year" data-year="2822">2822</div><div class="system-map-gallery-year" data-year="2830">2830</div><div class="system-map-gallery-year" data-year="2864">2864</div><div class="system-map-gallery-year" data-year="3025">3025</div><div class="system-map-gallery-year" data-year="3030">3030</div><div class="system-map-gallery-year" data-year="3040">3040</div><div class="system-map-gallery-year" data-year="3049">3049</div><div class="system-map-gallery-year" data-year="3052">3052</div><div class="system-map-gallery-year" data-year="3057">3057</div><div class="system-map-gallery-year" data-year="3058">3058</div><div class="system-map-gallery-year" data-year="3059">3059</div><div class="system-map-gallery-year" data-year="3063">3063</div><div class="system-map-gallery-year" data-year="3067">3067</div><div class="system-map-gallery-year" data-year="3068">3068</div><div class="system-map-gallery-year" data-year="3075">3075</div><div class="system-map-gallery-year" data-year="3081">3081</div><div class="system-map-gallery-year" data-year="3085">3085</div><div class="system-map-gallery-year" data-year="3095">3095</div><div class="system-map-gallery-year" data-year="3130">3130</div><div class="system-map-gallery-year" data-year="3135">3135</div><div class="system-map-gallery-year" data-year="3145">3145</div><div class="system-map-gallery-year selected" data-year="3151">3151</div></div><div class="system-map-gallery-images-container"><div class="system-map-gallery-images-left" style="visibility: visible;">←</div><div class="system-map-gallery-images"><div class="system-map-gallery-image-cont system-map-gallery-image-cont-curr"><a href="https://cfw.sarna.net/images/systems/1.4/3151/Hibuarius_3151.svg" target="_blank" class="system-map-gallery-link"><picture class="system-map-gallery-image"><source class="system-map-gallery-image-avif" type="image/avif" srcset="https://cfw.sarna.net/images/systems/1.4/avif/3151/Hibuarius_3151.250.avif"><source class="system-map-gallery-image-webp" type="image/webp" srcset="https://cfw.sarna.net/images/systems/1.4/webp/3151/Hibuarius_3151.250.webp"><img loading="lazy" decoding="async" class="system-map-gallery-image system-map-gallery-image-curr" src="https://cfw.sarna.net/images/systems/1.4/jpg/3151/Hibuarius_3151.250.jpg"></picture><div>3151</div></a></div></div><div class="system-map-gallery-images-right" style="visibility: hidden;">→</div></div><div style="clear:both"></div></div>
            <h2><span class="mw-headline" id="Nearby_Systems">Nearby Systems</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Hibuarius&amp;action=edit&amp;section=4" title="Edit section: Nearby Systems">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <table class="wikitable nearby-systems" style="background: #gray; text-align:center; border: 1px solid black;">
            <tbody><tr>
            <th colspan="8">Closest 36 systems (35 within 60 light-years)<br>Distance in light years, closest systems first:
            </th></tr>
            <tr>
            <td class="lessthan30"><a href="/wiki/Segerica" title="Segerica">Segerica</a>
            </td>
            <td class="lessthan30">12.0
            </td>
            <td class="lessthan30"><a href="/wiki/Jia_Tian" title="Jia Tian">Arn</a>
            </td>
            <td class="lessthan30">12.7
            </td>
            <td class="lessthan30"><a href="/wiki/Salardion" title="Salardion">Salardion</a>
            </td>
            <td class="lessthan30">16.1
            </td>
            <td class="lessthan30"><a href="/wiki/Aquagea" title="Aquagea">Aquagea</a>
            </td>
            <td class="lessthan30">19.8
            </td></tr>
            <tr>
            <td class="lessthan30"><a href="/wiki/Amnesty" title="Amnesty">Amnesty</a>
            </td>
            <td class="lessthan30">20.8
            </td>
            <td class="lessthan30"><a href="/wiki/Antias" title="Antias">Antias</a>
            </td>
            <td class="lessthan30">25.9
            </td>
            <td class="lessthan30"><a href="/wiki/Viribium" title="Viribium">Viribium</a>
            </td>
            <td class="lessthan30">26.3
            </td>
            <td class="lessthan30"><a href="/wiki/Liu%27s_Memory" title="Liu's Memory">Shaobuon</a>
            </td>
            <td class="lessthan30">28.4
            </td></tr>
            <tr>
            <td class="lessthan30"><a href="/wiki/Joppa" title="Joppa">Joppa</a>
            </td>
            <td class="lessthan30">29.5
            </td>
            <td class="over30"><a href="/wiki/Espia" title="Espia">Espia</a>
            </td>
            <td class="over30">30.3
            </td>
            <td class="over30"><a href="/wiki/New_Roland" title="New Roland">New Roland</a>
            </td>
            <td class="over30">32.1
            </td>
            <td class="over30"><a href="/wiki/Adrar" title="Adrar">Adrar</a>
            </td>
            <td class="over30">34.3
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Bonavista" title="Bonavista">Bonavista</a>
            </td>
            <td class="over30">35.9
            </td>
            <td class="over30"><a href="/wiki/Pilpala" title="Pilpala">Pilpala</a>
            </td>
            <td class="over30">38.5
            </td>
            <td class="over30"><a href="/wiki/Cassilda" title="Cassilda">Cassilda</a>
            </td>
            <td class="over30">41.1
            </td>
            <td class="over30"><a href="/wiki/Andarmax" title="Andarmax">Andarmax</a>
            </td>
            <td class="over30">42.8
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Claybrooke" title="Claybrooke">Claybrooke</a>
            </td>
            <td class="over30">43.7
            </td>
            <td class="over30"><a href="/wiki/Cluff%27s_Stand" title="Cluff's Stand">Zhaomaon</a>
            </td>
            <td class="over30">44.0
            </td>
            <td class="over30"><a href="/wiki/Hastur" title="Hastur">Hastur</a>
            </td>
            <td class="over30">45.1
            </td>
            <td class="over30"><a href="/wiki/Itica" title="Itica">Itica</a>
            </td>
            <td class="over30">45.4
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Thurrock" title="Thurrock">Thurrock</a>
            </td>
            <td class="over30">45.8
            </td>
            <td class="over30"><a href="/wiki/Alloway" title="Alloway">Alloway</a>
            </td>
            <td class="over30">47.7
            </td>
            <td class="over30"><a href="/wiki/Ghorepani" title="Ghorepani">Ghorepani</a>
            </td>
            <td class="over30">48.9
            </td>
            <td class="over30"><a href="/wiki/Naryn" title="Naryn">Naryn</a>
            </td>
            <td class="over30">49.6
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Zathras" title="Zathras">Zathras</a>
            </td>
            <td class="over30">49.7
            </td>
            <td class="over30"><a href="/wiki/Alamagordo" title="Alamagordo">Alamagordo</a>
            </td>
            <td class="over30">50.1
            </td>
            <td class="over30"><a href="/wiki/New_Abilene" title="New Abilene">New Abilene</a>
            </td>
            <td class="over30">51.7
            </td>
            <td class="over30"><a href="/wiki/Sax" title="Sax">Sax</a>
            </td>
            <td class="over30">51.8
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Calseraigne" title="Calseraigne">Calseraigne</a>
            </td>
            <td class="over30">52.7
            </td>
            <td class="over30"><a href="/wiki/Mattisskogen" title="Mattisskogen">Mattisskogen</a>
            </td>
            <td class="over30">53.3
            </td>
            <td class="over30"><a href="/wiki/Linhauiguan" title="Linhauiguan">Linhauiguan</a>
            </td>
            <td class="over30">56.5
            </td>
            <td class="over30"><a href="/wiki/Repulse" title="Repulse">Repulse</a>
            </td>
            <td class="over30">56.6
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Early_Dawn" title="Early Dawn">Early Dawn</a>
            </td>
            <td class="over30">57.1
            </td>
            <td class="over30"><a href="/wiki/Athna" title="Athna">Athna</a>
            </td>
            <td class="over30">57.7
            </td>
            <td class="over30"><a href="/wiki/Jacomarle" title="Jacomarle">Jacomarle</a>
            </td>
            <td class="over30">58.8
            </td>
            <td class="over60"><a href="/wiki/Chennai" title="Chennai">Chennai</a>
            </td>
            <td class="over60">60.9
            </td></tr>
            </tbody></table>
            <h2><span class="mw-headline" id="References">References</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Hibuarius&amp;action=edit&amp;section=5" title="Edit section: References">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <div class="mw-references-wrap mw-references-columns"><ol class="references">
            <li id="cite_note-HB:HLp17-1"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HLp17_1-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Liao</i>, p. 17, "Capellan Confederation Founding - [2366] Maü"</span>
            </li>
            <li id="cite_note-HB:HLp25-2"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HLp25_2-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Liao</i>, p. 25, "Capellan Confederation after Age of War - [2571] Map"</span>
            </li>
            <li id="cite_note-H:RWp159-3"><span class="mw-cite-backlink"><a href="#cite_ref-H:RWp159_3-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Reunification War</i>, p. 159, "Inner Sphere - [2596] Map"</span>
            </li>
            <li id="cite_note-ER:2750p37-4"><span class="mw-cite-backlink"><a href="#cite_ref-ER:2750p37_4-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 2750</i>, p. 37, "Inner Sphere - [2750] Map"</span>
            </li>
            <li id="cite_note-FM:SLDFpvii-5"><span class="mw-cite-backlink"><a href="#cite_ref-FM:SLDFpvii_5-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: SLDF</i>, p. vii, "Inner Sphere - [2764] Map"</span>
            </li>
            <li id="cite_note-H:LoTv1p11-6"><span class="mw-cite-backlink"><a href="#cite_ref-H:LoTv1p11_6-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Liberation of Terra Volume 1</i>, p. 11, "Inner Sphere - [2765] Map"</span>
            </li>
            <li id="cite_note-FSW.28SB.29p24-25-7"><span class="mw-cite-backlink"><a href="#cite_ref-FSW.28SB.29p24-25_7-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>First Succession War (sourcebook)</i>, pp. 24-25, "Inner Sphere - [2786] Map"</span>
            </li>
            <li id="cite_note-HB:HLp31-8"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HLp31_8-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Liao</i>, p. 31, "Capellan Confederation after First Succession War - [2822 Map]"</span>
            </li>
            <li id="cite_note-H:LoTV2p122-9"><span class="mw-cite-backlink"><a href="#cite_ref-H:LoTV2p122_9-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Liberation of Terra Volume 2</i>, p. 122-123, "Inner Sphere - [2822] Map"</span>
            </li>
            <li id="cite_note-FSW.28SB.29p112-113-10"><span class="mw-cite-backlink"><a href="#cite_ref-FSW.28SB.29p112-113_10-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>First Succession War (sourcebook)</i>, pp. 112-113, "Inner Sphere - [2822] Map"</span>
            </li>
            <li id="cite_note-HB:HLp39-11"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-HB:HLp39_11-0"><span class="cite-accessibility-label">Jump up to: </span>11.0</a></sup> <sup><a href="#cite_ref-HB:HLp39_11-1">11.1</a></sup></span> <span class="reference-text"><i>Handbook: House Liao</i>, p. 39, "Capellan Confederation after Second Succession War - [2864] Map"</span>
            </li>
            <li id="cite_note-HAtACp12-12"><span class="mw-cite-backlink"><a href="#cite_ref-HAtACp12_12-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>House Arano (The Aurigan Coalition)</i>, p. 12 "THE RIMWARD PERIPHERY - MAP PLACED ON FILE 21-07-2890"</span>
            </li>
            <li id="cite_note-HAtACp1s4-15-13"><span class="mw-cite-backlink"><a href="#cite_ref-HAtACp1s4-15_13-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>House Arano (The Aurigan Coalition)</i>, p. 14-15 "3022 INTERSTELLAR EXPEDITIONS. IAAS-349-SIFNM97-MMMXXII"</span>
            </li>
            <li id="cite_note-HB:HLp40-14"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-HB:HLp40_14-0"><span class="cite-accessibility-label">Jump up to: </span>14.0</a></sup> <sup><a href="#cite_ref-HB:HLp40_14-1">14.1</a></sup></span> <span class="reference-text"><i>Handbook: House Liao</i>, p. 40, "Capellan Confederation after Third Succession War - [3025] Map"</span>
            </li>
            <li id="cite_note-HB:HLp49-15"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HLp49_15-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Liao</i>, p. 49, "Capellan Confederation after Fourth Succession War - [3030] Map"</span>
            </li>
            <li id="cite_note-H:Wo3039p132-16"><span class="mw-cite-backlink"><a href="#cite_ref-H:Wo3039p132_16-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: War of 3039</i>, p. 132, "Inner Sphere - [3040] Map"</span>
            </li>
            <li id="cite_note-ER:3052p10-17"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3052p10_17-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3052</i>, p. 10, Inner Sphere - [3050] Map</span>
            </li>
            <li id="cite_note-ER:3052p22-18"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3052p22_18-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3052</i>, p. 22, Inner Sphere - [3052] Map</span>
            </li>
            <li id="cite_note-ER:3062p10-19"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3062p10_19-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3062</i>, p. 10, Inner Sphere - [3057] Map</span>
            </li>
            <li id="cite_note-HB:HLp60-20"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HLp60_20-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Liao</i>, p. 60, "Capellan Confederation after Operation Guerrero [3058]"</span>
            </li>
            <li id="cite_note-ER:3062p29-21"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3062p29_21-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3062</i>, p. 29, "Inner Sphere - [3063] Map"</span>
            </li>
            <li id="cite_note-HB:HLp68-22"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HLp68_22-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Liao</i>, p. 68, "Capellan Confederation after FedCom Civil War - [3067] Map"</span>
            </li>
            <li id="cite_note-J:FRp43-23"><span class="mw-cite-backlink"><a href="#cite_ref-J:FRp43_23-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Jihad: Final Reckoning</i>, p. 43, "Inner Sphere - [3067] Map"</span>
            </li>
            <li id="cite_note-JS:TBDp64-24"><span class="mw-cite-backlink"><a href="#cite_ref-JS:TBDp64_24-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Jihad Secrets: The Blake Documents</i>, p. 64, "Inner Sphere - [3075] Map"</span>
            </li>
            <li id="cite_note-FR:CCAFp21-25"><span class="mw-cite-backlink"><a href="#cite_ref-FR:CCAFp21_25-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Report: CCAF</i>, p. 21, "Capellan Confederation Armed Forces Deployment Map - [August 3079]"</span>
            </li>
            <li id="cite_note-J:FRp63-26"><span class="mw-cite-backlink"><a href="#cite_ref-J:FRp63_26-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Jihad: Final Reckoning</i>, p. 63, "Inner Sphere - [3081] Map"</span>
            </li>
            <li id="cite_note-FM:3085pvii-27"><span class="mw-cite-backlink"><a href="#cite_ref-FM:3085pvii_27-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: 3085</i>, p. vii , "Inner Sphere - [3085] Map"</span>
            </li>
            <li id="cite_note-ER:3145p10-28"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3145p10_28-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3145</i>, p. 10, "Inner Sphere - [3135] Map"</span>
            </li>
            <li id="cite_note-ER:3145p38-29"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3145p38_29-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3145</i>, p. 38, "Inner Sphere - [3145] Map"</span>
            </li>
            <li id="cite_note-FM:3145pVI-30"><span class="mw-cite-backlink"><a href="#cite_ref-FM:3145pVI_30-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: 3145</i>, p. VI, "Inner Sphere - [3145] Map"</span>
            </li>
            <li id="cite_note-BTVG-31"><span class="mw-cite-backlink"><a href="#cite_ref-BTVG_31-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text">"BattleTech Video Game"</span>
            </li>
            </ol></div>
            <h2><span class="mw-headline" id="Bibliography">Bibliography</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Hibuarius&amp;action=edit&amp;section=6" title="Edit section: Bibliography">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <ul><li><i><a href="/wiki/BattleTech_(Video_Game)" title="BattleTech (Video Game)">BattleTech (Video Game)</a></i></li>
            <li><i><a href="/wiki/Era_Report:_2750" title="Era Report: 2750">Era Report: 2750</a></i></li>
            <li><i><a href="/wiki/Era_Report:_3052" title="Era Report: 3052">Era Report: 3052</a></i></li>
            <li><i><a href="/wiki/Era_Report:_3062" title="Era Report: 3062">Era Report: 3062</a></i></li>
            <li><i><a href="/wiki/Era_Report:_3145" title="Era Report: 3145">Era Report: 3145</a></i></li>
            <li><i><a href="/wiki/Field_Manual:_3085" title="Field Manual: 3085">Field Manual: 3085</a></i></li>
            <li><i><a href="/wiki/Field_Manual:_3145" title="Field Manual: 3145">Field Manual: 3145</a></i></li>
            <li><i><a href="/wiki/Field_Manual:_SLDF" title="Field Manual: SLDF">Field Manual: SLDF</a></i></li>
            <li><i><a href="/wiki/Field_Report:_CCAF" title="Field Report: CCAF">Field Report: CCAF</a></i></li>
            <li><i><a href="/wiki/First_Succession_War_(sourcebook)" title="First Succession War (sourcebook)">First Succession War (sourcebook)</a></i></li>
            <li><i><a href="/wiki/Handbook:_House_Liao" title="Handbook: House Liao">Handbook: House Liao</a></i></li>
            <li><i><a href="/wiki/Historical:_Liberation_of_Terra_Volume_1" title="Historical: Liberation of Terra Volume 1">Historical: Liberation of Terra Volume 1</a></i></li>
            <li><i><a href="/wiki/Historical:_Liberation_of_Terra_Volume_2" title="Historical: Liberation of Terra Volume 2">Historical: Liberation of Terra Volume 2</a></i></li>
            <li><i><a href="/wiki/Historical:_Reunification_War" title="Historical: Reunification War">Historical: Reunification War</a></i></li>
            <li><i><a href="/wiki/Historical:_War_of_3039" title="Historical: War of 3039">Historical: War of 3039</a></i></li>
            <li><i><a href="/wiki/Jihad:_Final_Reckoning" title="Jihad: Final Reckoning">Jihad: Final Reckoning</a></i></li>
            <li><i><a href="/wiki/Jihad_Secrets:_The_Blake_Documents" title="Jihad Secrets: The Blake Documents">Jihad Secrets: The Blake Documents</a></i></li>
            <li><i><a href="/wiki/House_Arano_(The_Aurigan_Coalition)" title="House Arano (The Aurigan Coalition)">House Arano (The Aurigan Coalition)</a></i></li></ul>
            <!-- 
            NewPP limit report
            Cached time: 20240209062925
            Cache expiry: 2592000
            Dynamic content: false
            Complications: []
            [SMW] In‐text annotation parser time: 0 seconds
            CPU time usage: 0.133 seconds
            Real time usage: 0.352 seconds
            Preprocessor visited node count: 630/1000000
            Preprocessor generated node count: 0/1000000
            Post‐expand include size: 6241/2097152 bytes
            Template argument size: 425/2097152 bytes
            Highest expansion depth: 8/40
            Expensive parser function count: 0/200
            Unstrip recursion depth: 0/20
            Unstrip post‐expand size: 12467/5000000 bytes
            Lua time usage: 0.130/7 seconds
            Lua virtual size: 9.47 MB/50 MB
            Lua estimated memory usage: 0 bytes
            -->
            <!--
            Transclusion expansion time report (%,ms,calls,template)
            100.00%  265.927      1 -total
             75.45%  200.633      1 Template:InfoBoxSystem
             73.77%  196.165      1 Template:Infobox
              9.29%   24.716      1 Template:Div_col
              2.56%    6.811      1 Template:E
              2.41%    6.401      1 Template:Template_other
              1.38%    3.670      1 Template:Main_other
              1.20%    3.181      1 Template:Div_col_end
              1.17%    3.123      1 Template:ApocryphalContentStart
              1.17%    3.113      1 Template:ApocryphalContentEnd
            -->
            
            <!-- Saved in parser cache with key sarna_wiki-wiki_:pcache:idhash:45087-0!canonical and timestamp 20240209062925 and revision id 996588
             -->
            </div><h2></h2></div><div class="printfooter">
            Retrieved from "<a dir="ltr" href="https://www.sarna.net/wiki/index.php?title=Hibuarius&amp;oldid=996588">https://www.sarna.net/wiki/index.php?title=Hibuarius&amp;oldid=996588</a>"</div>
                        <div id="catlinks" class="catlinks" data-mw="interface"><div id="mw-normal-catlinks" class="mw-normal-catlinks"><a href="/wiki/Special:Categories" title="Special:Categories">Categories</a>: <ul><li><a href="/wiki/Category:Systems" title="Category:Systems">Systems</a></li><li><a href="/wiki/Category:Planets" title="Category:Planets">Planets</a></li><li><a href="/wiki/Category:Capellan_Confederation_Systems" title="Category:Capellan Confederation Systems">Capellan Confederation Systems</a></li></ul></div><div id="mw-hidden-catlinks" class="mw-hidden-catlinks mw-hidden-cats-hidden">Hidden categories: <ul><li><a href="/wiki/Category:Systems_with_undetermined_Spectral_class" title="Category:Systems with undetermined Spectral class">Systems with undetermined Spectral class</a></li><li><a href="/wiki/Category:Systems_with_undetermined_Recharge_time" title="Category:Systems with undetermined Recharge time">Systems with undetermined Recharge time</a></li><li><a href="/wiki/Category:Systems_with_undetermined_number_of_Recharge_stations" title="Category:Systems with undetermined number of Recharge stations">Systems with undetermined number of Recharge stations</a></li><li><a href="/wiki/Category:Systems_with_undetermined_number_of_Planets" title="Category:Systems with undetermined number of Planets">Systems with undetermined number of Planets</a></li></ul></div></div>            <!-- end content -->
                                    <div class="visualClear"></div>
                    </div>
               </div>
            """;

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(26);
        actual.CountFactions("No record").Should().Be(18);
        actual.CountFactions(("Capellan Confederation")).Should().Be(7);
        actual.CountFactions(("Independent world")).Should().Be(1);
    }

    /// <summary>
    /// https://www.sarna.net/wiki/Blackjack_(system)
    /// </summary>
    [Fact]
    public void Should_ParseSystemBlackjack()
    {
        var page = """
                   <div id="content">
                           <a id="top"></a>
                       
                               <script>window.sarnaAdProvider = 6;</script>
                           <script>
                           if (window.MutationObserver) {
                               var wrapper = document.getElementById('content');
                               var observer = new MutationObserver(function(mutations, observer) {
                                   wrapper.style.height = '';
                                   wrapper.style.minHeight = '';
                               });
                               observer.observe(wrapper, {
                                   attributes: true,
                                   attributeFilter: ['style']
                               });
                           }
                           </script>
                           <script data-cfasync="false" type="text/javascript">
                           var freestar = freestar || {};
                           freestar.queue = freestar.queue || [];
                           freestar.config = freestar.config || {};
                           freestar.config.enabled_slots = [];
                           </script>
                           <div id="p-banner-top-b" class="portlet">
                               <center>
                                                       <script data-cfasync="false" type="text/javascript">
                                       freestar.initCallback = function () { (freestar.config.enabled_slots.length === 0) ? freestar.initCallbackCalled = false : freestar.newAdSlots(freestar.config.enabled_slots) }
                                       </script>
                                       <script src="https://a.pub.network/sarna-net/pubfig.min.js" async=""></script>
                                       <div align="center" data-freestar-ad="__728x90" id="sarna_leaderboard_atf">
                                           <script data-cfasync="false" type="text/javascript">
                                           freestar.config.enabled_slots.push({ placementName: "sarna_leaderboard_atf", slotId: "sarna_leaderboard_atf" });
                                           </script>
                                       </div>
                                       <!-- Quantcast Choice. Consent Manager Tag v2.0 (for TCF 2.0) -->
                                       <script type="text/javascript" async="true">
                                       (function initQuantcast() {
                                           !function(){var e=document.createElement("script"),t=document.getElementsByTagName("script")[0],a="https://cmp.quantcast.com".concat("/choice/","kevTBasPvBb4b","/","sarna.net","/choice.js?tag_version=V2"),n=0;e.async=!0,e.type="text/javascript",e.src=a,t.parentNode.insertBefore(e,t),function(){for(var e,t="__tcfapiLocator",a=[],n=window;n;){try{if(n.frames[t]){e=n;break}}catch(e){}if(n===window.top)break;n=n.parent}e||(function e(){var a=n.document,i=!!n.frames[t];if(!i)if(a.body){var s=a.createElement("iframe");s.style.cssText="display:none",s.name=t,a.body.appendChild(s)}else setTimeout(e,5);return!i}(),n.__tcfapi=function(){var e,t=arguments;if(!t.length)return a;if("setGdprApplies"===t[0])t.length>3&&2===t[2]&&"boolean"==typeof t[3]&&(e=t[3],"function"==typeof t[2]&&t[2]("set",!0));else if("ping"===t[0]){var n={gdprApplies:e,cmpLoaded:!1,cmpStatus:"stub"};"function"==typeof t[2]&&t[2](n)}else"init"===t[0]&&"object"==typeof t[3]&&(t[3]=Object.assign(t[3],{tag_version:"V2"})),a.push(t)},n.addEventListener("message",function(e){var t="string"==typeof e.data,a={};try{a=t?JSON.parse(e.data):e.data}catch(e){}var n=a.__tcfapiCall;n&&window.__tcfapi(n.command,n.version,function(a,i){var s={__tcfapiReturn:{returnValue:a,success:i,callId:n.callId}};t&&(s=JSON.stringify(s)),e&&e.source&&e.source.postMessage&&e.source.postMessage(s,"*")},n.parameter)},!1))}();var i=function(){var e=arguments;typeof window.__uspapi!==i&&setTimeout(function(){void 0!==window.__uspapi&&window.__uspapi.apply(window.__uspapi,e)},500)};if(void 0===window.__uspapi){window.__uspapi=i;var s=setInterval(function(){n++,window.__uspapi===i&&n<3?console.warn("USP is not accessible"):clearInterval(s)},6e3)}}();
                                       })();
                                       </script>
                                                   </center>
                           </div>
                                   <div id="socialIcons">
                               <div class="share-bar">
                                   <div class="share-bar-service share-bar-service-facebook">
                                       <a class="social-share-frame" href="https://www.facebook.com/dialog/share?app_id=165706373584987&amp;display=popup&amp;href=https://www.sarna.net//wiki/Blackjack_(system)">
                                           <i class="fab fa-facebook-f"></i>
                                           <span class="share-bar-service-name">Like</span>
                                       </a>
                                   </div>
                                   <div class="share-bar-service share-bar-service-twitter">
                                       <a class="social-share-frame" href="https://twitter.com/share?url=https://www.sarna.net//wiki/Blackjack_(system)&amp;text=Blackjack (system)">
                                           <i class="fab fa-twitter"></i>
                                           <span class="share-bar-service-name">Tweet</span>
                                       </a>
                                   </div>
                                   <div class="clearfix"></div>
                               </div>
                           </div>
                               <h1 id="firstHeading" class="firstHeading">Blackjack (system)</h1>
                               <div id="right-column">
                                               <div id="right-ad-1" class="ad-container" style="width: 300px; height: 250px;">
                                       <div align="center" data-freestar-ad="__300x250 __300x250" id="sarna_medrec_right_1">
                                           <script data-cfasync="false" type="text/javascript">
                                           freestar.config.enabled_slots.push({ placementName: "sarna_medrec_right_1", slotId: "sarna_medrec_right_1" });
                                           </script>
                                       </div>
                                   </div>
                                   
                               <div style="width: 300px; margin-top: 10px" class="portlet" id="right-news">
                                   <h5>Sarna News</h5>
                                                           <a href="https://www.sarna.net/news/your-battletech-news-round-up-for-january-2024/">
                                               <img height="154" width="300" src="//cfw.sarna.net/news/wp-content/uploads/2024/01/Nightstar-Oil-Painting-by-1001WingedHussars.jpeg" style="max-width: 280px; height: auto; margin: 5px 10px" alt="Sarna.net News: Your BattleTech News Round-Up For January, 2024">
                                           </a>
                                       
                                   <!-- mailing list signup form -->
                                   <form action="//sarna.us17.list-manage.com/subscribe/post?u=56a7c808c4fc6faa708b0e488&amp;id=bd6a480de0" method="post" id="mc-embedded-subscribe-form" name="mc-embedded-subscribe-form" class="validate" target="_blank">
                                       <input type="text" value="" name="EMAIL" class="required email" placeholder="email" autocomplete="email" id="mce-EMAIL" style="width: 210px">
                                       <input type="submit" value="Sub!" name="subscribe" id="mc-embedded-subscribe" class="btn" style="width: 75px">
                                   </form>
                   
                                   <ul>
                                                       <li><a href="https://www.sarna.net/news/your-battletech-news-round-up-for-january-2024/" rel="bookmark">
                                           Your BattleTech News Round-Up For January, 2024                    </a></li>
                                                           <li><a href="https://www.sarna.net/news/battletech-in-2024-an-interview-with-line-developer-ray-arrastia-assistant-line-developer-aaron-cahall/" rel="bookmark">
                                           BattleTech In 2024 - An Interview With Line Developer Ray Arrastia &amp; Assistant Line Developer Aaron Cahall                    </a></li>
                                                           <li><a href="https://www.sarna.net/news/bad-mechs-hellfire/" rel="bookmark">
                                           Bad ‘Mechs - Hellfire                    </a></li>
                                                           <li><a href="https://www.sarna.net/news/sarnas-50000-article-celebration/" rel="bookmark">
                                           Sarna's 50,000 Article Celebration!                    </a></li>
                                                           <li><a href="https://www.sarna.net/news/your-battletech-news-round-up-for-december-2023/" rel="bookmark">
                                           Your BattleTech News Round-Up For December, 2023                    </a></li>
                                                           <li><a href="/news/">Read more →</a>
                                   </li></ul>
                               </div>
                   
                                                                           <div id="right-ad-2" class="ad-container" style="width: 300px; height: 250px; margin-top: 10px">
                                               <div align="center" data-freestar-ad="__300x250 __300x250" id="sarna_medrec_right_2">
                                                   <script data-cfasync="false" type="text/javascript">
                                                   freestar.config.enabled_slots.push({ placementName: "sarna_medrec_right_2", slotId: "sarna_medrec_right_2" });
                                                   </script>
                                               </div>
                                           </div>
                                                           
                               <div id="right-self-ad" class="right-ad" style="margin-top: 5px; width: 300px; height: 250px; text-align: center">
                                   <a href="https://www.sarna.net/wiki/BattleTechWiki:Support#Sarna_Merch"><img src="//cfw.sarna.net/images/ads/amazon-merch-sarna-white-hoodie-250x234.jpg" height="234" width="250" alt="Sarna.net Merch!"></a>
                                   <p><a href="https://www.sarna.net/wiki/BattleTechWiki:Support#Sarna_Merch">Support sarna.net!  Merch!</a></p>
                               </div>
                   
                               <div id="right-community-ad" class="right-ad" style="margin-top: 5px; width: 300px; height: 350px; text-align: center">
                                   <h3><a href="https://www.sarna.net/wiki/BattleTechWiki:CommunityAds">BattleTech Community Ads</a></h3>
                   
                                                       <a href="http://xmarx.com/"><img src="//cfw.sarna.net/images/ads/xmarx.png" width="300" height="300" alt="XMarx.com"></a>
                                       <p><a href="http://xmarx.com/">Xmarx Scale Terrain/Buildings</a></p>
                                                   </div>
                   
                                                                           <div id="right-ad-3" class="ad-container" style="width: 300px; height: 250px; margin-top: 10px">
                                               <div align="center" data-freestar-ad="__300x250" id="sarna_medrec_right_3">
                                                   <script data-cfasync="false" type="text/javascript">
                                                   freestar.config.enabled_slots.push({ placementName: "sarna_medrec_right_3", slotId: "sarna_medrec_right_3" });
                                                   </script>
                                               </div>
                                           </div>
                                                                   </div>
                               <div id="bodyContent" style="margin-right: 310px">
                           <div>
                               <div id="contentSub"></div>
                                       <!-- start content -->
                       <div id="mw-content-text" lang="en" dir="ltr" class="mw-content-ltr"><div class="mw-notification-area mw-notification-area-floating" id="mw-notification-area" style="display: none;"></div><div class="mw-parser-output"><table class="box-update_needed plainlinks metadata ambox ambox-notice" role="presentation" style="width: auto; margin-right: 0px;"><tbody><tr><td class="mbox-image"><div style="width:52px"><img alt="" src="https://cfw.sarna.net/wiki/images/1/1d/Information_icon4.svg?timestamp=20230302182457" width="40" height="40"></div></td><td class="mbox-text" style="margin: 0 10%;width: auto;"><div class="mbox-text-span"><div>This article needs to be updated with material from <i><a href="/wiki/Tamar_Rising" title="Tamar Rising">Tamar Rising</a></i>. Once these titles clear the <a href="/wiki/Policy:Moratorium" title="Policy:Moratorium">Moratorium period</a>, or if they already have, please consider revisiting this article and updating it with the new material.</div><span class="hide-when-compact"><i> (<small><a href="/wiki/Help:Maintenance_template_removal" title="Help:Maintenance template removal">Learn how and when to remove this template message</a></small>)</i></span></div></td></tr></tbody></table>
                   <dl><dd><i>This article is about the planetary system. For other uses, see <a href="/wiki/Blackjack" class="mw-disambig" title="Blackjack">Blackjack</a>.</i></dd></dl>
                   <p><br>
                   </p>
                   <style data-mw-deduplicate="TemplateStyles:r1004727">.mw-parser-output .infobox-subbox{padding:0;border:none;margin:-3px;width:auto;min-width:100%;font-size:100%;clear:none;float:none;background-color:transparent}.mw-parser-output .infobox-3cols-child{margin:auto}.mw-parser-output .infobox .navbar{font-size:100%}body.skin-minerva .mw-parser-output .infobox-header,body.skin-minerva .mw-parser-output .infobox-subheader,body.skin-minerva .mw-parser-output .infobox-above,body.skin-minerva .mw-parser-output .infobox-title,body.skin-minerva .mw-parser-output .infobox-image,body.skin-minerva .mw-parser-output .infobox-full-data,body.skin-minerva .mw-parser-output .infobox-below{text-align:center}</style><table class="infobox"><tbody><tr><th colspan="2" class="infobox-above" style="background:#c5d07d; border:0.15em solid #222; padding:0.2em;">Blackjack</th></tr><tr><td colspan="2" class="infobox-subheader" style="border:0.15em solid #222; padding:0.2em;"><div class="infoboxnavlink"><div class="system-nav-left">← 3135</div> <div class="system-nav-current">3151</div> <div class="system-nav-right"></div></div></td></tr><tr><td colspan="2" class="infobox-image" style="border:0.15em solid #222; padding:0.2em;"><div class="center"><div class="floatnone"><a href="/wiki/File:Blackjack_(system)_3151.svg" class="image"><img alt="Blackjack (system) 3151.svg" src="https://cfw.sarna.net/wiki/images/d/d8/Blackjack_%28system%29_3151.svg?timestamp=20210827183140" width="240" height="274"></a></div></div><div class="infobox-caption">Blackjack <a href="#Nearby_Systems">nearby systems</a><br>(<a href="/wiki/BattleTechWiki:Map_Legend" title="BattleTechWiki:Map Legend">Map Legend</a>)</div></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">System Information</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">X:Y Coordinates</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">-156.905&nbsp;: 364.43<sup class="noprint" style="white-space:nowrap">[<i><a href="/wiki/BattleTechWiki:System_coordinates" title="BattleTechWiki:System coordinates">e</a></i>]</sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Spectral class</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">G3V<sup id="cite_ref-HB:HSp80-81_1-0" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-0" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-0" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Recharge time</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">184 hours<sup id="cite_ref-HB:HSp80-81_1-1" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-1" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-1" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Recharge station(s)</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">None<sup id="cite_ref-HB:HSp80-81_1-2" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-2" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-2" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Planet(s)</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">7<sup id="cite_ref-TRp102_4-0" class="reference"><a href="#cite_note-TRp102-4">[4]</a></sup></td></tr></tbody></table>
                   <p>The <b>Blackjack</b> system was home to at least one habitable world, <b>Blackjack III</b>, and as of June <a href="/wiki/3152" title="3152">3152</a> was located in the <a href="/wiki/Alyina_Mercantile_League" title="Alyina Mercantile League">Alyina Mercantile League</a><sup id="cite_ref-TRp72_5-0" class="reference"><a href="#cite_note-TRp72-5">[5]</a></sup>
                   </p>
                   <div id="toc" class="toc"><input type="checkbox" role="button" id="toctogglecheckbox" class="toctogglecheckbox" style="display:none"><div class="toctitle" lang="en" dir="ltr"><h2>Contents</h2><span class="toctogglespan"><label class="toctogglelabel" for="toctogglecheckbox"></label></span></div>
                   <ul>
                   <li class="toclevel-1 tocsection-1"><a href="#System_Description"><span class="tocnumber">1</span> <span class="toctext">System Description</span></a></li>
                   <li class="toclevel-1 tocsection-2"><a href="#System_History"><span class="tocnumber">2</span> <span class="toctext">System History</span></a></li>
                   <li class="toclevel-1 tocsection-3"><a href="#Political_Affiliation"><span class="tocnumber">3</span> <span class="toctext">Political Affiliation</span></a></li>
                   <li class="toclevel-1 tocsection-4"><a href="#Blackjack_III"><span class="tocnumber">4</span> <span class="toctext">Blackjack III</span></a>
                   <ul>
                   <li class="toclevel-2 tocsection-5"><a href="#Planetary_History"><span class="tocnumber">4.1</span> <span class="toctext">Planetary History</span></a>
                   <ul>
                   <li class="toclevel-3 tocsection-6"><a href="#Early_History"><span class="tocnumber">4.1.1</span> <span class="toctext">Early History</span></a></li>
                   <li class="toclevel-3 tocsection-7"><a href="#Clan_Invasion"><span class="tocnumber">4.1.2</span> <span class="toctext">Clan Invasion</span></a></li>
                   <li class="toclevel-3 tocsection-8"><a href="#Lyran_Alliance_Invasion"><span class="tocnumber">4.1.3</span> <span class="toctext">Lyran Alliance Invasion</span></a></li>
                   <li class="toclevel-3 tocsection-9"><a href="#Second_Falcon_Incursion"><span class="tocnumber">4.1.4</span> <span class="toctext">Second Falcon Incursion</span></a></li>
                   <li class="toclevel-3 tocsection-10"><a href="#Dark_Age"><span class="tocnumber">4.1.5</span> <span class="toctext">Dark Age</span></a></li>
                   </ul>
                   </li>
                   <li class="toclevel-2 tocsection-11"><a href="#Military_Deployment"><span class="tocnumber">4.2</span> <span class="toctext">Military Deployment</span></a>
                   <ul>
                   <li class="toclevel-3 tocsection-12"><a href="#3038"><span class="tocnumber">4.2.1</span> <span class="toctext">3038</span></a></li>
                   <li class="toclevel-3 tocsection-13"><a href="#3050"><span class="tocnumber">4.2.2</span> <span class="toctext">3050</span></a></li>
                   <li class="toclevel-3 tocsection-14"><a href="#3054.E2.80.933061"><span class="tocnumber">4.2.3</span> <span class="toctext">3054–3061</span></a></li>
                   <li class="toclevel-3 tocsection-15"><a href="#3067"><span class="tocnumber">4.2.4</span> <span class="toctext">3067</span></a></li>
                   </ul>
                   </li>
                   <li class="toclevel-2 tocsection-16"><a href="#Geography"><span class="tocnumber">4.3</span> <span class="toctext">Geography</span></a></li>
                   <li class="toclevel-2 tocsection-17"><a href="#Planetary_Locations"><span class="tocnumber">4.4</span> <span class="toctext">Planetary Locations</span></a></li>
                   </ul>
                   </li>
                   <li class="toclevel-1 tocsection-18"><a href="#Map_Gallery"><span class="tocnumber">5</span> <span class="toctext">Map Gallery</span></a></li>
                   <li class="toclevel-1 tocsection-19"><a href="#Nearby_Systems"><span class="tocnumber">6</span> <span class="toctext">Nearby Systems</span></a></li>
                   <li class="toclevel-1 tocsection-20"><a href="#References"><span class="tocnumber">7</span> <span class="toctext">References</span></a></li>
                   <li class="toclevel-1 tocsection-21"><a href="#Bibliography"><span class="tocnumber">8</span> <span class="toctext">Bibliography</span></a></li>
                   </ul>
                   </div>
                   
                   <div class="sarna_inline_mobile_ad"><div id="nn_mobile_mpu1"></div><div align="center" data-freestar-ad="__300x250" id="sarna_mobile_incontent_1"><script data-cfasync="false" type="text/javascript">if(window.freestar){freestar.config.enabled_slots.push({ placementName: 'sarna_mobile_incontent_1', slotId: 'sarna_mobile_incontent_1' });}</script></div></div><h2><span class="mw-headline" id="System_Description">System Description</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=1" title="Edit section: System Description">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
                   <p>Blackjack was located near the <a href="/wiki/Goat_Path" title="Goat Path">Goat Path</a> and <a href="/wiki/Hot_Springs" title="Hot Springs">Hot Springs</a> systems<sup id="cite_ref-ER:3145p39_6-0" class="reference"><a href="#cite_note-ER:3145p39-6">[6]</a></sup><sup id="cite_ref-FM:3145pVI_7-0" class="reference"><a href="#cite_note-FM:3145pVI-7">[7]</a></sup> and consists of a class G3V primary orbited by at least three planets.<sup id="cite_ref-HB:HSp80-81_1-3" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-3" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-3" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup>
                   </p>
                   <h2><span class="mw-headline" id="System_History">System History</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=2" title="Edit section: System History">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
                   <p>Blackjack was settled during or shortly before the <a href="/wiki/Age_of_War" title="Age of War">Age of War</a>.<sup id="cite_ref-HB:HSp13_8-0" class="reference"><a href="#cite_note-HB:HSp13-8">[8]</a></sup><sup id="cite_ref-HB:HSp25_9-0" class="reference"><a href="#cite_note-HB:HSp25-9">[9]</a></sup>
                   </p>
                   <div class="sarna_inline_mobile_ad"><div id="nn_mobile_mpu2"></div><div align="center" data-freestar-ad="__300x250" id="sarna_mobile_incontent_2"><script data-cfasync="false" type="text/javascript">if(window.freestar){freestar.config.enabled_slots.push({ placementName: 'sarna_mobile_incontent_2', slotId: 'sarna_mobile_incontent_2' });}</script></div></div><h2><span class="mw-headline" id="Political_Affiliation">Political Affiliation</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=3" title="Edit section: Political Affiliation">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
                   <style data-mw-deduplicate="TemplateStyles:r983948">.mw-parser-output .div-col{margin-top:0.3em;column-width:30em}.mw-parser-output .div-col-small{font-size:90%}.mw-parser-output .div-col-rules{column-rule:1px solid #aaa}.mw-parser-output .div-col dl,.mw-parser-output .div-col ol,.mw-parser-output .div-col ul{margin-top:0}.mw-parser-output .div-col li,.mw-parser-output .div-col dd{page-break-inside:avoid;break-inside:avoid-column}.mw-parser-output .plainlist ol,.mw-parser-output .plainlist ul{line-height:inherit;list-style:none;margin:0}.mw-parser-output .plainlist ol li,.mw-parser-output .plainlist ul li{margin-bottom:0}</style><div class="div-col" style="column-width: 25em;">
                   <ul><li><a href="/wiki/2341" title="2341">2341</a> - No record<sup id="cite_ref-HB:HSp13_8-1" class="reference"><a href="#cite_note-HB:HSp13-8">[8]</a></sup></li>
                   <li><a href="/wiki/2571" title="2571">2571</a> - <a href="/wiki/Lyran_Commonwealth" title="Lyran Commonwealth">Lyran Commonwealth</a><sup id="cite_ref-HB:HSp25_9-1" class="reference"><a href="#cite_note-HB:HSp25-9">[9]</a></sup></li>
                   <li><a href="/wiki/2596" title="2596">2596</a> - Lyran Commonwealth<sup id="cite_ref-HRWp158_10-0" class="reference"><a href="#cite_note-HRWp158-10">[10]</a></sup></li>
                   <li><a href="/wiki/2750" title="2750">2750</a> - Lyran Commonwealth<sup id="cite_ref-HB:MPSp25_11-0" class="reference"><a href="#cite_note-HB:MPSp25-11">[11]</a></sup><sup id="cite_ref-ER:2750p36_12-0" class="reference"><a href="#cite_note-ER:2750p36-12">[12]</a></sup></li>
                   <li><a href="/wiki/2765" title="2765">2765</a> - Lyran Commonwealth<sup id="cite_ref-H:LoTV1p10_13-0" class="reference"><a href="#cite_note-H:LoTV1p10-13">[13]</a></sup></li>
                   <li><a href="/wiki/2822" title="2822">2822</a> - Lyran Commonwealth<sup id="cite_ref-14" class="reference"><a href="#cite_note-14">[14]</a></sup></li>
                   <li><a href="/wiki/2864" title="2864">2864</a> - Lyran Commonwealth<sup id="cite_ref-15" class="reference"><a href="#cite_note-15">[15]</a></sup></li>
                   <li><a href="/wiki/3025" title="3025">3025</a> - Lyran Commonwealth<sup id="cite_ref-16" class="reference"><a href="#cite_note-16">[16]</a></sup></li>
                   <li><a href="/wiki/3030" title="3030">3030</a> - Lyran Commonwealth<sup id="cite_ref-17" class="reference"><a href="#cite_note-17">[17]</a></sup></li>
                   <li><a href="/wiki/3040" title="3040">3040</a> - <a href="/wiki/Federated_Commonwealth" title="Federated Commonwealth">Federated Commonwealth</a><sup id="cite_ref-18" class="reference"><a href="#cite_note-18">[18]</a></sup><sup id="cite_ref-19" class="reference"><a href="#cite_note-19">[19]</a></sup></li>
                   <li><a href="/wiki/3050" title="3050">3050</a> - <a href="/wiki/Clan_Jade_Falcon" title="Clan Jade Falcon">Clan Jade Falcon</a> (from August)<sup id="cite_ref-HB:HSp80-81_1-4" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup></li>
                   <li><a href="/wiki/3052" title="3052">3052</a> - <a href="/wiki/Clan_Steel_Viper" title="Clan Steel Viper">Clan Steel Viper</a><sup id="cite_ref-JFSB_20-0" class="reference"><a href="#cite_note-JFSB-20">[20]</a></sup><sup id="cite_ref-21" class="reference"><a href="#cite_note-21">[21]</a></sup><sup id="cite_ref-22" class="reference"><a href="#cite_note-22">[22]</a></sup></li>
                   <li><a href="/wiki/3057" title="3057">3057</a> - Clan Jade Falcon/Clan Steel Viper<sup id="cite_ref-23" class="reference"><a href="#cite_note-23">[23]</a></sup></li>
                   <li><a href="/wiki/3063" title="3063">3063</a> - Clan Jade Falcon<sup id="cite_ref-24" class="reference"><a href="#cite_note-24">[24]</a></sup></li>
                   <li><a href="/wiki/3064" title="3064">3064</a> - <a href="/wiki/Lyran_Alliance" class="mw-redirect" title="Lyran Alliance">Lyran Alliance</a> (from December)<sup id="cite_ref-FCCW_25-0" class="reference"><a href="#cite_note-FCCW-25">[25]</a></sup></li>
                   <li><a href="/wiki/3067" title="3067">3067</a> - Lyran Alliance<sup id="cite_ref-26" class="reference"><a href="#cite_note-26">[26]</a></sup></li>
                   <li><a href="/wiki/3069" title="3069">3069</a> - Clan Jade Falcon (from February)<sup id="cite_ref-J:FRp46_27-0" class="reference"><a href="#cite_note-J:FRp46-27">[27]</a></sup></li>
                   <li><a href="/wiki/3075" title="3075">3075</a> - Clan Jade Falcon<sup id="cite_ref-28" class="reference"><a href="#cite_note-28">[28]</a></sup></li>
                   <li><a href="/wiki/3079" title="3079">3079</a> - Clan Jade Falcon<sup id="cite_ref-29" class="reference"><a href="#cite_note-29">[29]</a></sup></li>
                   <li><a href="/wiki/3081" title="3081">3081</a> - Clan Jade Falcon<sup id="cite_ref-30" class="reference"><a href="#cite_note-30">[30]</a></sup></li>
                   <li><a href="/wiki/3085" title="3085">3085</a> - Clan Jade Falcon<sup id="cite_ref-31" class="reference"><a href="#cite_note-31">[31]</a></sup></li>
                   <li><a href="/wiki/3135" title="3135">3135</a> - Clan Jade Falcon<sup id="cite_ref-ER:3145p11_32-0" class="reference"><a href="#cite_note-ER:3145p11-32">[32]</a></sup></li>
                   <li><a href="/wiki/3145" title="3145">3145</a> - Clan Jade Falcon<sup id="cite_ref-ER:3145p39_6-1" class="reference"><a href="#cite_note-ER:3145p39-6">[6]</a></sup><sup id="cite_ref-FM:3145pVI_7-1" class="reference"><a href="#cite_note-FM:3145pVI-7">[7]</a></sup></li>
                   <li><a href="/wiki/3151" title="3151">3151</a> - Clan Jade Falcon<sup id="cite_ref-SFp102_33-0" class="reference"><a href="#cite_note-SFp102-33">[33]</a></sup></li>
                   <li><a href="/wiki/3152" title="3152">3152</a> - <a href="/wiki/Alyina_Mercantile_League" title="Alyina Mercantile League">Alyina Mercantile League</a><sup id="cite_ref-TRpm_34-0" class="reference"><a href="#cite_note-TRpm-34">[34]</a></sup><sup id="cite_ref-TRp72_5-1" class="reference"><a href="#cite_note-TRp72-5">[5]</a></sup></li></ul>
                   </div>
                   <hr>
                   <h2><span class="mw-headline" id="Blackjack_III">Blackjack III</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=4" title="Edit section: Blackjack III">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
                   <link rel="mw-deduplicated-inline-style" href="mw-data:TemplateStyles:r1004727"><table class="infobox"><tbody><tr><th colspan="2" class="infobox-above" style="background:#c5d07d; border:0.15em solid #222; padding:0.2em;">Blackjack III</th></tr><tr><td colspan="2" class="infobox-image" style="border:0.15em solid #222; padding:0.2em;"><div class="center"><div class="floatnone"><a href="/wiki/File:Blackjack_Flag.jpg" class="image"><img alt="Blackjack Flag.jpg" src="https://cfw.sarna.net/wiki/images/9/9d/Blackjack_Flag.jpg?timestamp=20230903035514" decoding="async" loading="lazy" width="174" height="137"></a></div></div></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">Astrophysical</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">System position</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Third<sup id="cite_ref-HB:HSp80-81_1-5" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-4" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-4" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Jump Point distance</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">8.53 days<sup id="cite_ref-HB:HSp80-81_1-6" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-5" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-5" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Moons</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">2 (Club, Spade)<sup id="cite_ref-HB:HSp80-81_1-7" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-6" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-6" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">Geophysical</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Surface gravity</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">0.97<sup id="cite_ref-HB:HSp80-81_1-8" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-7" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-7" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Atmospheric pressure</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Standard (Breathable)<sup id="cite_ref-HB:HSp80-81_1-9" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-8" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-8" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Equatorial temperature</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">49°C (Arid-Desert),<sup id="cite_ref-HB:HSp80-81_1-10" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-9" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-9" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup><br>45°C (Arid-Desert, 3150)<sup id="cite_ref-TRp102_4-1" class="reference"><a href="#cite_note-TRp102-4">[4]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Surface water</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">31%<sup id="cite_ref-HB:HSp80-81_1-11" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-10" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-10" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Highest native life</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Reptile<sup id="cite_ref-HB:HSp80-81_1-12" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-11" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-11" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Landmasses</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">3 (Diamond, Orbule, Vada)<sup id="cite_ref-O:TCp14_3-12" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">History and Culture</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Population</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">1,076,000,000 (3067),<sup id="cite_ref-HB:HSp80-81_1-13" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><br>1,105,000,000 (3076–3079),<sup id="cite_ref-M.26M_2-12" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-13" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup><br>907,000,000 (3150)<sup id="cite_ref-TRp102_4-2" class="reference"><a href="#cite_note-TRp102-4">[4]</a></sup></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">Government and Infrastructure</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Capital</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">Lott's Revenge<sup id="cite_ref-HB:HSp80-81_1-14" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-O:TCp14_3-14" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">HPG Class</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">B<sup id="cite_ref-HB:HSp80-81_1-15" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-13" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-15" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></td></tr></tbody></table>
                   <p>Blackjack III, more commonly referred to simply as Blackjack, is the third planet in the Black jack system and has two moons named Club and Spade.<sup id="cite_ref-HB:HSp80-81_1-16" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup><sup id="cite_ref-M.26M_2-14" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup><sup id="cite_ref-O:TCp14_3-16" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup>
                   </p>
                   <h3><span class="mw-headline" id="Planetary_History">Planetary History</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=5" title="Edit section: Planetary History">edit</a><span class="mw-editsection-bracket">]</span></span></h3>
                   <h4><span class="mw-headline" id="Early_History">Early History</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=6" title="Edit section: Early History">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <p>Blackjack was settled by English and French settlers from <a href="/wiki/Terra" title="Terra">Terra</a>. These early pioneers had come to Blackjack due to its accessible mineral wealth. Though ores were in limited qualities, heavy industry had built up on the planet. By the <a href="/wiki/Second_Succession_War" title="Second Succession War">Second Succession War</a>, the valuable ores had dried up and its industries were destroyed by <a href="/wiki/Draconis_Combine" title="Draconis Combine">Combine</a> and Periphery Raiders.<sup id="cite_ref-HB:HSp80-81_1-17" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup>
                   </p><p>The planet became infamously known to being the home of the Blackjack School of Conflict, which continued to operate well into the mid-<a href="/wiki/Thirty-first_century" class="mw-redirect" title="Thirty-first century">thirty-first century</a>.
                   </p>
                   <h4><span class="mw-headline" id="Clan_Invasion">Clan Invasion</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=7" title="Edit section: Clan Invasion">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <p>In August <a href="/wiki/3050" title="3050">3050</a>, Clan Jade Falcon's <a href="/wiki/Delta_Galaxy_(Clan_Jade_Falcon)" title="Delta Galaxy (Clan Jade Falcon)">Delta Galaxy</a> invaded the planet in Wave Four of <a href="/wiki/Operation_REVIVAL" class="mw-redirect" title="Operation REVIVAL">Operation REVIVAL</a>.  In the <a href="/wiki/Trial_of_Possession" title="Trial of Possession">Trial of Possession</a> for Blackjack, the planet's only defenders was the Blackjack School's <a href="/wiki/Blackjack_Training_Battalion" title="Blackjack Training Battalion">Training Battalion</a> led by <a href="/wiki/Kommandant" class="mw-redirect" title="Kommandant">Kommandant</a> <a href="/wiki/Dean_Bristow" title="Dean Bristow">Dean Bristow</a>.  While fighting the <a href="/wiki/Second_Falcon_Jaegers_(Clan_Jade_Falcon)" class="mw-redirect" title="Second Falcon Jaegers (Clan Jade Falcon)">Second Falcon Jaegers</a>, the training battalion suffered 80 percent casualties.  However, the Falcons were so impressed by the cadets' actions they let most of the survivors retreat from the planet, but they did take some cadets that they saw as honorable as <a href="/wiki/Bondsmen" class="mw-redirect" title="Bondsmen">Bondsmen</a>.  In one example, Cadet <a href="/wiki/Mark_Harris" title="Mark Harris">Mark Harris</a> defeated a Clan <a href="/wiki/MechWarrior_(pilot)" title="MechWarrior (pilot)">MechWarrior</a> in a <a href="/wiki/Trial_of_Possession" title="Trial of Possession">Trial of Possession</a> for Harris' family home and later would be seen to have been accepted into the Falcon <a href="/wiki/Touman" title="Touman">Touman</a>.<sup id="cite_ref-HB:HSp80-81_1-18" class="reference"><a href="#cite_note-HB:HSp80-81-1">[1]</a></sup>
                   </p><p>In <a href="/wiki/3052" title="3052">3052</a>, Clan Steel Viper was given control of the planet and shortly after razed the Blackjack School.<sup id="cite_ref-JFSB_20-1" class="reference"><a href="#cite_note-JFSB-20">[20]</a></sup>
                   </p>
                   <h4><span class="mw-headline" id="Lyran_Alliance_Invasion">Lyran Alliance Invasion</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=8" title="Edit section: Lyran Alliance Invasion">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <p>During <a href="/wiki/Jade_Falcon_Incursion_(3064)" title="Jade Falcon Incursion (3064)">Clan Jade Falcon's Incursion</a> in the Lyran space, coalition of Lyran Alliance forces had counterattacked the Falcons' Occupation Zone.  In December <a href="/wiki/3064" title="3064">3064</a> <a href="/wiki/Archer_Christifori" title="Archer Christifori">General Christifori</a> led the <a href="/wiki/Archer%27s_Avengers" class="mw-redirect" title="Archer's Avengers">Archer's Avengers</a> in a <a href="/wiki/Trial_of_Possession" title="Trial of Possession">Trial of Possession</a> for Blackjack. The Lyran forces had set up their base in the ruins of the Blackjack School. In the course of the battle with the defending <a href="/wiki/8th_Provisional_Garrison_(Clan_Jade_Falcon)" title="8th Provisional Garrison (Clan Jade Falcon)">Eighth</a> and <a href="/wiki/10th_Provisional_Garrison_(Clan_Jade_Falcon)" title="10th Provisional Garrison (Clan Jade Falcon)">Tenth Provisional Garrison Clusters</a> the Trial of Possession had turned into a <a href="/wiki/Trial_of_Annihilation" title="Trial of Annihilation">Trial of Annihilation</a> due to events of various <a href="/wiki/Regiment" class="mw-redirect" title="Regiment">regiments</a> on <a href="/wiki/Twycross" title="Twycross">Twycross</a>. In orbit above Blackjack the Lyran forces had an allied <a href="/wiki/Clan_Wolf_(in_Exile)" class="mw-redirect" title="Clan Wolf (in Exile)">Clan Wolf in Exile's</a> <a href="/wiki/WarShip" title="WarShip">WarShip</a>, the <i><a href="/wiki/Aegis" class="mw-redirect" title="Aegis">Aegis</a></i>-class heavy cruiser <a href="/wiki/Black_Paw_(Individual_Aegis-class_WarShip)" title="Black Paw (Individual Aegis-class WarShip)">CWS <i>Black Paw</i></a>, providing support for them. The <i>Black Paw</i> engaged in battle with a Jade Falcon WarShip, the <i>Aegis</i>-class <a href="/wiki/White_Talon" class="mw-redirect" title="White Talon">CJF <i>White Talon</i></a> during conflict for Blackjack. The <i>Black Paw</i> also provided orbit-to-ground fire support for the Lyran troops. Six days into the conflict, General <a href="/wiki/Adam_Steiner" title="Adam Steiner">Adam Steiner</a> arrived with reinforcements and assisted the increasingly weary Archer's Avengers. The Trial ended shortly after <a href="/wiki/SaKhan" title="SaKhan">saKhan</a> <a href="/wiki/Samantha_Clees" title="Samantha Clees">Samantha Clees</a> arrived in-system and declared the Lyrans winner of the Trial.<sup id="cite_ref-FCCW_25-1" class="reference"><a href="#cite_note-FCCW-25">[25]</a></sup>
                   </p>
                   <h4><span class="mw-headline" id="Second_Falcon_Incursion">Second Falcon Incursion</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=9" title="Edit section: Second Falcon Incursion">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <p>Although the Lyran Alliance held the world in the late 3060s, they did little to actually improve the life of their former countrymen. The inhabitants of Blackjack maintained the Clan caste system they'd been living under for two decades, and the Alliance couldn't spare the money or manpower to rehabilitate them back into the Spheroid way of life.<sup id="cite_ref-M.26M_2-15" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup> The Falcons invaded again in late January <a href="/wiki/3069" title="3069">3069</a>,<sup id="cite_ref-J:FRp46_27-1" class="reference"><a href="#cite_note-J:FRp46-27">[27]</a></sup> and this time the Alliance simply left the world in their hands after putting up a token fight.<sup id="cite_ref-M.26M_2-16" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup>
                   </p><p>After retaking the planet the Jade Falcons rebuilt the Blackjack School of Combat as the <a href="/wiki/Jade_Falcon_School_of_Conflict_on_Blackjack" title="Jade Falcon School of Conflict on Blackjack">Jade Falcon School of Conflict on Blackjack</a>. On this campus the Clan maintained its traditional warrior training programs, with trueborns raised in <i><a href="/wiki/Sibko" title="Sibko">sibkos</a></i> and freeborns able to apply at age 13. Unlike other Falcon training sites, however, the Jade Falcon School of Combat on Blackjack also had a militia training facility. This was a much shorter program and trained its students to guard worlds and resources that didn't warrant frontline or second-line troops from the <a href="/wiki/Clan_Jade_Falcon_Touman" title="Clan Jade Falcon Touman">Falcon <i>touman</i></a>. The militia program's focus was on driving off raiding forces and operating in search-and-rescue operations.<sup id="cite_ref-M.26M_2-17" class="reference"><a href="#cite_note-M.26M-2">[2]</a></sup>
                   </p>
                   <h4><span class="mw-headline" id="Dark_Age">Dark Age</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=10" title="Edit section: Dark Age">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <p>In April <a href="/wiki/3147" title="3147">3147</a>, a pirate gang attacked the School of Conflict and was met by a defending force comprised mostly of <i>sibko</i> cadets piloting <a href="/wiki/Howler_(Baboon)" title="Howler (Baboon)">Howlers</a>. To the mixed pride and chagrin of their instructor, an avowed opponent of <a href="/wiki/Malvina_Hazen" title="Malvina Hazen">Malvina Hazen's</a> <a href="/wiki/Mongol_Doctrine" title="Mongol Doctrine">Mongol Doctrine</a>, the cadets successfully drove off the pirates using aggressive Mongol tactics, though incurring heavy equipment losses in the process.<sup id="cite_ref-35" class="reference"><a href="#cite_note-35">[35]</a></sup>
                   </p>
                   <h3><span class="mw-headline" id="Military_Deployment">Military Deployment</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=11" title="Edit section: Military Deployment">edit</a><span class="mw-editsection-bracket">]</span></span></h3>
                   <style data-mw-deduplicate="TemplateStyles:r949308">@media(min-width:720px){.mw-parser-output .columns-start .column{float:left;min-width:20em}.mw-parser-output .columns-2 .column{width:50%}.mw-parser-output .columns-3 .column{width:33.3%}.mw-parser-output .columns-4 .column{width:25%}.mw-parser-output .columns-5 .column{width:20%}}</style><div class="columns-start columns-3"><div class="column">
                   <h4><span class="mw-headline" id="3038">3038</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=12" title="Edit section: 3038">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <ul><li><a href="/wiki/Blackjack_Training_Battalion" title="Blackjack Training Battalion">Blackjack Training Battalion</a><sup id="cite_ref-36" class="reference"><a href="#cite_note-36">[36]</a></sup></li></ul>
                   <h4><span class="mw-headline" id="3050">3050</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=13" title="Edit section: 3050">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <ul><li>Blackjack Training Battalion<sup id="cite_ref-20YU24_37-0" class="reference"><a href="#cite_note-20YU24-37">[37]</a></sup></li></ul>
                   <link rel="mw-deduplicated-inline-style" href="mw-data:TemplateStyles:r949308"></div><div class="column">
                   <h4><span id="3054–3061"></span><span class="mw-headline" id="3054.E2.80.933061">3054–3061</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=14" title="Edit section: 3054–3061">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <ul><li><a href="/wiki/Eightieth_Fang_(Clan_Steel_Viper)" class="mw-redirect" title="Eightieth Fang (Clan Steel Viper)">Eightieth Fang</a><sup id="cite_ref-OR58_38-0" class="reference"><a href="#cite_note-OR58-38">[38]</a></sup><sup id="cite_ref-FMWC166_39-0" class="reference"><a href="#cite_note-FMWC166-39">[39]</a></sup></li></ul>
                   <h4><span class="mw-headline" id="3067">3067</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=15" title="Edit section: 3067">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
                   <ul><li><a href="/wiki/Rubinsky%27s_Light_Horse" title="Rubinsky's Light Horse">Rubinsky's Light Horse</a><sup id="cite_ref-40" class="reference"><a href="#cite_note-40">[40]</a></sup></li></ul>
                   </div><div style="clear: both"></div></div>
                   <h3><span class="mw-headline" id="Geography">Geography</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=16" title="Edit section: Geography">edit</a><span class="mw-editsection-bracket">]</span></span></h3>
                   <p>Blackjack has three continents named Diamond, Orbule, and Vada.<sup id="cite_ref-O:TCp14_3-17" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup>
                   </p>
                   <h3><span class="mw-headline" id="Planetary_Locations">Planetary Locations</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=17" title="Edit section: Planetary Locations">edit</a><span class="mw-editsection-bracket">]</span></span></h3>
                   <ul><li><a href="/wiki/Blackjack_School_of_Conflict" class="mw-redirect" title="Blackjack School of Conflict">Blackjack School of Conflict</a>: House Steiner Military academy, which was unique due to being the Commonwealth's only privately owned military school. The school was notorious for its courses shadier classes in less professional military skills. The School was razed in 3052 by Clan Steel Viper.<sup id="cite_ref-JFSB_20-2" class="reference"><a href="#cite_note-JFSB-20">[20]</a></sup> The School was subsequently rebuilt as the Jade Falcon School of Combat during the Jihad.</li>
                   <li>Lott's Revenge: the planetary capital city, located on Diamond.<sup id="cite_ref-O:TCp14_3-18" class="reference"><a href="#cite_note-O:TCp14-3">[3]</a></sup></li></ul>
                   <h2><span class="mw-headline" id="Map_Gallery">Map Gallery</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=18" title="Edit section: Map Gallery">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
                   <div class="system-map-gallery"><div class="system-map-gallery-years"><div class="system-map-gallery-year-header">Years:</div><div class="system-map-gallery-year" data-year="2571">2571</div><div class="system-map-gallery-year" data-year="2596">2596</div><div class="system-map-gallery-year" data-year="2750">2750</div><div class="system-map-gallery-year" data-year="2765">2765</div><div class="system-map-gallery-year" data-year="2767">2767</div><div class="system-map-gallery-year" data-year="2783">2783</div><div class="system-map-gallery-year" data-year="2786">2786</div><div class="system-map-gallery-year" data-year="2821">2821</div><div class="system-map-gallery-year" data-year="2822">2822</div><div class="system-map-gallery-year" data-year="2830">2830</div><div class="system-map-gallery-year" data-year="2864">2864</div><div class="system-map-gallery-year" data-year="3025">3025</div><div class="system-map-gallery-year" data-year="3030">3030</div><div class="system-map-gallery-year" data-year="3040">3040</div><div class="system-map-gallery-year" data-year="3049">3049</div><div class="system-map-gallery-year" data-year="3052">3052</div><div class="system-map-gallery-year" data-year="3057">3057</div><div class="system-map-gallery-year" data-year="3058">3058</div><div class="system-map-gallery-year" data-year="3059">3059</div><div class="system-map-gallery-year" data-year="3063">3063</div><div class="system-map-gallery-year" data-year="3067">3067</div><div class="system-map-gallery-year" data-year="3068">3068</div><div class="system-map-gallery-year" data-year="3075">3075</div><div class="system-map-gallery-year" data-year="3081">3081</div><div class="system-map-gallery-year" data-year="3085">3085</div><div class="system-map-gallery-year" data-year="3095">3095</div><div class="system-map-gallery-year" data-year="3130">3130</div><div class="system-map-gallery-year selected" data-year="3135">3135</div><div class="system-map-gallery-year selected" data-year="3145">3145</div><div class="system-map-gallery-year selected" data-year="3151">3151</div></div><div class="system-map-gallery-images-container"><div class="system-map-gallery-images-left" style="visibility: visible;">←</div><div class="system-map-gallery-images"><div class="system-map-gallery-image-cont system-map-gallery-image-cont-prev"><a href="https://cfw.sarna.net/images/systems/1.4/3135/Blackjack_(system)_3135.svg" target="_blank" class="system-map-gallery-link"><picture class="system-map-gallery-image"><source class="system-map-gallery-image-avif" type="image/avif" srcset="https://cfw.sarna.net/images/systems/1.4/avif/3135/Blackjack_(system)_3135.250.avif"><source class="system-map-gallery-image-webp" type="image/webp" srcset="https://cfw.sarna.net/images/systems/1.4/webp/3135/Blackjack_(system)_3135.250.webp"><img loading="lazy" decoding="async" class="system-map-gallery-image system-map-gallery-image-prev" src="https://cfw.sarna.net/images/systems/1.4/jpg/3135/Blackjack_(system)_3135.250.jpg"></picture><div>3135</div></a></div><div class="system-map-gallery-image-cont system-map-gallery-image-cont-curr"><a href="https://cfw.sarna.net/images/systems/1.4/3145/Blackjack_(system)_3145.svg" target="_blank" class="system-map-gallery-link"><picture class="system-map-gallery-image"><source class="system-map-gallery-image-avif" type="image/avif" srcset="https://cfw.sarna.net/images/systems/1.4/avif/3145/Blackjack_(system)_3145.250.avif"><source class="system-map-gallery-image-webp" type="image/webp" srcset="https://cfw.sarna.net/images/systems/1.4/webp/3145/Blackjack_(system)_3145.250.webp"><img loading="lazy" decoding="async" class="system-map-gallery-image system-map-gallery-image-curr" src="https://cfw.sarna.net/images/systems/1.4/jpg/3145/Blackjack_(system)_3145.250.jpg"></picture><div>3145</div></a></div><div class="system-map-gallery-image-cont system-map-gallery-image-cont-next"><a href="https://cfw.sarna.net/images/systems/1.4/3151/Blackjack_(system)_3151.svg" target="_blank" class="system-map-gallery-link"><picture class="system-map-gallery-image"><source class="system-map-gallery-image-avif" type="image/avif" srcset="https://cfw.sarna.net/images/systems/1.4/avif/3151/Blackjack_(system)_3151.250.avif"><source class="system-map-gallery-image-webp" type="image/webp" srcset="https://cfw.sarna.net/images/systems/1.4/webp/3151/Blackjack_(system)_3151.250.webp"><img loading="lazy" decoding="async" class="system-map-gallery-image system-map-gallery-image-next" src="https://cfw.sarna.net/images/systems/1.4/jpg/3151/Blackjack_(system)_3151.250.jpg"></picture><div>3151</div></a></div></div><div class="system-map-gallery-images-right" style="visibility: hidden;">→</div></div><div style="clear:both"></div></div>
                   <h2><span class="mw-headline" id="Nearby_Systems">Nearby Systems</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=19" title="Edit section: Nearby Systems">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
                   <table class="wikitable nearby-systems" style="background: #gray; text-align:center; border: 1px solid black;">
                   <tbody><tr>
                   <th colspan="8">Closest 32 systems (30 within 60 light-years)<br>Distance in light years, closest systems first:
                   </th></tr>
                   <tr>
                   <td class="lessthan30"><a href="/wiki/Goat_Path" title="Goat Path">Goat Path</a>
                   </td>
                   <td class="lessthan30">11.2
                   </td>
                   <td class="lessthan30"><a href="/wiki/Hot_Springs" title="Hot Springs">Hot Springs</a>
                   </td>
                   <td class="lessthan30">14.1
                   </td>
                   <td class="lessthan30"><a href="/wiki/Malibu" title="Malibu">Malibu</a>
                   </td>
                   <td class="lessthan30">17.7
                   </td>
                   <td class="lessthan30"><a href="/wiki/Beta" title="Beta">Beta</a>
                   </td>
                   <td class="lessthan30">20.0
                   </td></tr>
                   <tr>
                   <td class="lessthan30"><a href="/wiki/Kooken%27s_Pleasure_Pit" title="Kooken's Pleasure Pit">Kookens Pleasure Pit</a>
                   </td>
                   <td class="lessthan30">20.4
                   </td>
                   <td class="lessthan30"><a href="/wiki/Waldorff" title="Waldorff">Waldorff</a>
                   </td>
                   <td class="lessthan30">25.7
                   </td>
                   <td class="lessthan30"><a href="/wiki/Roadside" title="Roadside">Roadside</a>
                   </td>
                   <td class="lessthan30">28.5
                   </td>
                   <td class="over30"><a href="/wiki/Alyina" title="Alyina">Alyina</a>
                   </td>
                   <td class="over30">32.2
                   </td></tr>
                   <tr>
                   <td class="over30"><a href="/wiki/Butler" title="Butler">Butler</a>
                   </td>
                   <td class="over30">32.6
                   </td>
                   <td class="over30"><a href="/wiki/Pasig" title="Pasig">Pasig</a>
                   </td>
                   <td class="over30">32.9
                   </td>
                   <td class="over30"><a href="/wiki/Blue_Hole" title="Blue Hole">Blue Hole</a>
                   </td>
                   <td class="over30">33.7
                   </td>
                   <td class="over30"><a href="/wiki/Golandrinas" title="Golandrinas">Golandrinas</a>
                   </td>
                   <td class="over30">36.0
                   </td></tr>
                   <tr>
                   <td class="over30"><a href="/wiki/Biegga" title="Biegga">Biegga</a>
                   </td>
                   <td class="over30">37.4
                   </td>
                   <td class="over30"><a href="/wiki/Black_Earth" title="Black Earth">Black Earth</a>
                   </td>
                   <td class="over30">37.5
                   </td>
                   <td class="over30"><a href="/wiki/Derf" title="Derf">Derf</a>
                   </td>
                   <td class="over30">39.9
                   </td>
                   <td class="over30"><a href="/wiki/Wotan" title="Wotan">Wotan</a>
                   </td>
                   <td class="over30">46.4
                   </td></tr>
                   <tr>
                   <td class="over30"><a href="/wiki/Kikuyu" title="Kikuyu">Kikuyu</a>
                   </td>
                   <td class="over30">46.8
                   </td>
                   <td class="over30"><a href="/wiki/Denizli" title="Denizli">Denizli</a>
                   </td>
                   <td class="over30">47.1
                   </td>
                   <td class="over30"><a href="/wiki/Apolakkia" title="Apolakkia">Apolakkia</a>
                   </td>
                   <td class="over30">47.1
                   </td>
                   <td class="over30"><a href="/wiki/Mkuranga" title="Mkuranga">Mkuranga</a>
                   </td>
                   <td class="over30">47.2
                   </td></tr>
                   <tr>
                   <td class="over30"><a href="/wiki/Renren" title="Renren">Renren</a>
                   </td>
                   <td class="over30">47.3
                   </td>
                   <td class="over30"><a href="/wiki/Chahar" title="Chahar">Chahar</a>
                   </td>
                   <td class="over30">47.9
                   </td>
                   <td class="over30"><a href="/wiki/Parakoila" title="Parakoila">Parakoila</a>
                   </td>
                   <td class="over30">48.3
                   </td>
                   <td class="over30"><a href="/wiki/Trell" title="Trell">Trell</a>
                   </td>
                   <td class="over30">48.5
                   </td></tr>
                   <tr>
                   <td class="over30"><a href="/wiki/Twycross" title="Twycross">Twycross</a>
                   </td>
                   <td class="over30">48.6
                   </td>
                   <td class="over30"><a href="/wiki/Manx" title="Manx">Manx</a>
                   </td>
                   <td class="over30">53.7
                   </td>
                   <td class="over30"><a href="/wiki/Clermont" title="Clermont">Clermont</a>
                   </td>
                   <td class="over30">54.4
                   </td>
                   <td class="over30"><a href="/wiki/Devin" title="Devin">Devin</a>
                   </td>
                   <td class="over30">55.4
                   </td></tr>
                   <tr>
                   <td class="over30"><a href="/wiki/Mogyorod" title="Mogyorod">Mogyorod</a>
                   </td>
                   <td class="over30">58.5
                   </td>
                   <td class="over30"><a href="/wiki/Babaeski" title="Babaeski">Babaeski</a>
                   </td>
                   <td class="over30">59.8
                   </td>
                   <td class="over60"><a href="/wiki/Winfield_(LC)" title="Winfield (LC)">Treeline</a>
                   </td>
                   <td class="over60">60.9
                   </td>
                   <td class="over60"><a href="/wiki/Somerset" title="Somerset">Somerset</a>
                   </td>
                   <td class="over60">61.3
                   </td></tr>
                   </tbody></table>
                   <h2><span class="mw-headline" id="References">References</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=20" title="Edit section: References">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
                   <div class="mw-references-wrap mw-references-columns"><ol class="references">
                   <li id="cite_note-HB:HSp80-81-1"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-HB:HSp80-81_1-0"><span class="cite-accessibility-label">Jump up to: </span>1.00</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-1">1.01</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-2">1.02</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-3">1.03</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-4">1.04</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-5">1.05</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-6">1.06</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-7">1.07</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-8">1.08</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-9">1.09</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-10">1.10</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-11">1.11</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-12">1.12</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-13">1.13</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-14">1.14</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-15">1.15</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-16">1.16</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-17">1.17</a></sup> <sup><a href="#cite_ref-HB:HSp80-81_1-18">1.18</a></sup></span> <span class="reference-text"><i>Handbook: House Steiner</i>, pp. 80–81: "Blackjack Planet Profile"</span>
                   </li>
                   <li id="cite_note-M.26M-2"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-M.26M_2-0"><span class="cite-accessibility-label">Jump up to: </span>2.00</a></sup> <sup><a href="#cite_ref-M.26M_2-1">2.01</a></sup> <sup><a href="#cite_ref-M.26M_2-2">2.02</a></sup> <sup><a href="#cite_ref-M.26M_2-3">2.03</a></sup> <sup><a href="#cite_ref-M.26M_2-4">2.04</a></sup> <sup><a href="#cite_ref-M.26M_2-5">2.05</a></sup> <sup><a href="#cite_ref-M.26M_2-6">2.06</a></sup> <sup><a href="#cite_ref-M.26M_2-7">2.07</a></sup> <sup><a href="#cite_ref-M.26M_2-8">2.08</a></sup> <sup><a href="#cite_ref-M.26M_2-9">2.09</a></sup> <sup><a href="#cite_ref-M.26M_2-10">2.10</a></sup> <sup><a href="#cite_ref-M.26M_2-11">2.11</a></sup> <sup><a href="#cite_ref-M.26M_2-12">2.12</a></sup> <sup><a href="#cite_ref-M.26M_2-13">2.13</a></sup> <sup><a href="#cite_ref-M.26M_2-14">2.14</a></sup> <sup><a href="#cite_ref-M.26M_2-15">2.15</a></sup> <sup><a href="#cite_ref-M.26M_2-16">2.16</a></sup> <sup><a href="#cite_ref-M.26M_2-17">2.17</a></sup></span> <span class="reference-text"><i>Masters and Minions: The StarCorps Dossiers</i>, p. 134: "Clan Jade Falcon Key Worlds"</span>
                   </li>
                   <li id="cite_note-O:TCp14-3"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-O:TCp14_3-0"><span class="cite-accessibility-label">Jump up to: </span>3.00</a></sup> <sup><a href="#cite_ref-O:TCp14_3-1">3.01</a></sup> <sup><a href="#cite_ref-O:TCp14_3-2">3.02</a></sup> <sup><a href="#cite_ref-O:TCp14_3-3">3.03</a></sup> <sup><a href="#cite_ref-O:TCp14_3-4">3.04</a></sup> <sup><a href="#cite_ref-O:TCp14_3-5">3.05</a></sup> <sup><a href="#cite_ref-O:TCp14_3-6">3.06</a></sup> <sup><a href="#cite_ref-O:TCp14_3-7">3.07</a></sup> <sup><a href="#cite_ref-O:TCp14_3-8">3.08</a></sup> <sup><a href="#cite_ref-O:TCp14_3-9">3.09</a></sup> <sup><a href="#cite_ref-O:TCp14_3-10">3.10</a></sup> <sup><a href="#cite_ref-O:TCp14_3-11">3.11</a></sup> <sup><a href="#cite_ref-O:TCp14_3-12">3.12</a></sup> <sup><a href="#cite_ref-O:TCp14_3-13">3.13</a></sup> <sup><a href="#cite_ref-O:TCp14_3-14">3.14</a></sup> <sup><a href="#cite_ref-O:TCp14_3-15">3.15</a></sup> <sup><a href="#cite_ref-O:TCp14_3-16">3.16</a></sup> <sup><a href="#cite_ref-O:TCp14_3-17">3.17</a></sup> <sup><a href="#cite_ref-O:TCp14_3-18">3.18</a></sup></span> <span class="reference-text"><i>Objectives: The Clans</i>, p. 14: "Blackjack"</span>
                   </li>
                   <li id="cite_note-TRp102-4"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-TRp102_4-0"><span class="cite-accessibility-label">Jump up to: </span>4.0</a></sup> <sup><a href="#cite_ref-TRp102_4-1">4.1</a></sup> <sup><a href="#cite_ref-TRp102_4-2">4.2</a></sup></span> <span class="reference-text"><i>Tamar Rising</i>, p. 102</span>
                   </li>
                   <li id="cite_note-TRp72-5"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-TRp72_5-0"><span class="cite-accessibility-label">Jump up to: </span>5.0</a></sup> <sup><a href="#cite_ref-TRp72_5-1">5.1</a></sup></span> <span class="reference-text"><i>Tamar Rising</i>, p. 72</span>
                   </li>
                   <li id="cite_note-ER:3145p39-6"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-ER:3145p39_6-0"><span class="cite-accessibility-label">Jump up to: </span>6.0</a></sup> <sup><a href="#cite_ref-ER:3145p39_6-1">6.1</a></sup></span> <span class="reference-text"><i>Era Report: 3145</i>, p. 39</span>
                   </li>
                   <li id="cite_note-FM:3145pVI-7"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-FM:3145pVI_7-0"><span class="cite-accessibility-label">Jump up to: </span>7.0</a></sup> <sup><a href="#cite_ref-FM:3145pVI_7-1">7.1</a></sup></span> <span class="reference-text"><i>Field Manual: 3145</i>, p. VI: "Inner Sphere - 3145"</span>
                   </li>
                   <li id="cite_note-HB:HSp13-8"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-HB:HSp13_8-0"><span class="cite-accessibility-label">Jump up to: </span>8.0</a></sup> <sup><a href="#cite_ref-HB:HSp13_8-1">8.1</a></sup></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 13</span>
                   </li>
                   <li id="cite_note-HB:HSp25-9"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-HB:HSp25_9-0"><span class="cite-accessibility-label">Jump up to: </span>9.0</a></sup> <sup><a href="#cite_ref-HB:HSp25_9-1">9.1</a></sup></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 25</span>
                   </li>
                   <li id="cite_note-HRWp158-10"><span class="mw-cite-backlink"><a href="#cite_ref-HRWp158_10-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Reunification War</i>, p. 158</span>
                   </li>
                   <li id="cite_note-HB:MPSp25-11"><span class="mw-cite-backlink"><a href="#cite_ref-HB:MPSp25_11-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: Major Periphery States</i>, p. 25</span>
                   </li>
                   <li id="cite_note-ER:2750p36-12"><span class="mw-cite-backlink"><a href="#cite_ref-ER:2750p36_12-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 2750</i>, p. 36</span>
                   </li>
                   <li id="cite_note-H:LoTV1p10-13"><span class="mw-cite-backlink"><a href="#cite_ref-H:LoTV1p10_13-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Liberation of Terra Volume 1</i>, p. 10</span>
                   </li>
                   <li id="cite_note-14"><span class="mw-cite-backlink"><a href="#cite_ref-14" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 36</span>
                   </li>
                   <li id="cite_note-15"><span class="mw-cite-backlink"><a href="#cite_ref-15" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 40</span>
                   </li>
                   <li id="cite_note-16"><span class="mw-cite-backlink"><a href="#cite_ref-16" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 47</span>
                   </li>
                   <li id="cite_note-17"><span class="mw-cite-backlink"><a href="#cite_ref-17" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 56</span>
                   </li>
                   <li id="cite_note-18"><span class="mw-cite-backlink"><a href="#cite_ref-18" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 59</span>
                   </li>
                   <li id="cite_note-19"><span class="mw-cite-backlink"><a href="#cite_ref-19" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: War of 3039</i>, p. 132</span>
                   </li>
                   <li id="cite_note-JFSB-20"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-JFSB_20-0"><span class="cite-accessibility-label">Jump up to: </span>20.0</a></sup> <sup><a href="#cite_ref-JFSB_20-1">20.1</a></sup> <sup><a href="#cite_ref-JFSB_20-2">20.2</a></sup></span> <span class="reference-text"><i>Jade Falcon Sourcebook</i>, p. 41: Blackjack - Trial of Possession ends in defeat for Blackjack school students &amp; school gets destroyed later</span>
                   </li>
                   <li id="cite_note-21"><span class="mw-cite-backlink"><a href="#cite_ref-21" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 61</span>
                   </li>
                   <li id="cite_note-22"><span class="mw-cite-backlink"><a href="#cite_ref-22" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3052</i>, p. 23</span>
                   </li>
                   <li id="cite_note-23"><span class="mw-cite-backlink"><a href="#cite_ref-23" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3062</i>, p. 11</span>
                   </li>
                   <li id="cite_note-24"><span class="mw-cite-backlink"><a href="#cite_ref-24" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3062</i>, p. 29</span>
                   </li>
                   <li id="cite_note-FCCW-25"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-FCCW_25-0"><span class="cite-accessibility-label">Jump up to: </span>25.0</a></sup> <sup><a href="#cite_ref-FCCW_25-1">25.1</a></sup></span> <span class="reference-text"><i>FedCom Civil War</i>, p. 118: "Blackjack"</span>
                   </li>
                   <li id="cite_note-26"><span class="mw-cite-backlink"><a href="#cite_ref-26" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 70</span>
                   </li>
                   <li id="cite_note-J:FRp46-27"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-J:FRp46_27-0"><span class="cite-accessibility-label">Jump up to: </span>27.0</a></sup> <sup><a href="#cite_ref-J:FRp46_27-1">27.1</a></sup></span> <span class="reference-text"><i>Jihad: Final Reckoning</i>, p. 46: "The Jihad in Review"</span>
                   </li>
                   <li id="cite_note-28"><span class="mw-cite-backlink"><a href="#cite_ref-28" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Jihad Secrets: The Blake Documents</i>, p. 65</span>
                   </li>
                   <li id="cite_note-29"><span class="mw-cite-backlink"><a href="#cite_ref-29" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Report: LAAF</i>, p. 19</span>
                   </li>
                   <li id="cite_note-30"><span class="mw-cite-backlink"><a href="#cite_ref-30" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Jihad: Final Reckoning</i>, p. 63</span>
                   </li>
                   <li id="cite_note-31"><span class="mw-cite-backlink"><a href="#cite_ref-31" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: 3085</i>, p. 127</span>
                   </li>
                   <li id="cite_note-ER:3145p11-32"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3145p11_32-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3145</i>, p. 11</span>
                   </li>
                   <li id="cite_note-SFp102-33"><span class="mw-cite-backlink"><a href="#cite_ref-SFp102_33-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Shattered Fortress</i>, p. 102</span>
                   </li>
                   <li id="cite_note-TRpm-34"><span class="mw-cite-backlink"><a href="#cite_ref-TRpm_34-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Tamar Rising</i>, poster map</span>
                   </li>
                   <li id="cite_note-35"><span class="mw-cite-backlink"><a href="#cite_ref-35" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Recognition Guide: ilClan, vol. 8</i>, p. 4</span>
                   </li>
                   <li id="cite_note-36"><span class="mw-cite-backlink"><a href="#cite_ref-36" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: War of 3039</i>, p. 138: "Deployment Table"</span>
                   </li>
                   <li id="cite_note-20YU24-37"><span class="mw-cite-backlink"><a href="#cite_ref-20YU24_37-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>20 Year Update</i>, p. 24: "Federated Commonwealth Deployment Table"</span>
                   </li>
                   <li id="cite_note-OR58-38"><span class="mw-cite-backlink"><a href="#cite_ref-OR58_38-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Objective Raids</i>, p. 58: "Unit Note"</span>
                   </li>
                   <li id="cite_note-FMWC166-39"><span class="mw-cite-backlink"><a href="#cite_ref-FMWC166_39-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: Warden Clans</i>, p. 166: "Warden Clans Deployment Table"</span>
                   </li>
                   <li id="cite_note-40"><span class="mw-cite-backlink"><a href="#cite_ref-40" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Mercenaries Supplemental</i>, p. 68: Mercenary Employment Table</span>
                   </li>
                   </ol></div>
                   <h2><span class="mw-headline" id="Bibliography">Bibliography</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Blackjack_(system)&amp;action=edit&amp;section=21" title="Edit section: Bibliography">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
                   <link rel="mw-deduplicated-inline-style" href="mw-data:TemplateStyles:r983948"><div class="div-col" style="column-width: 25em;">
                   <ul><li><i><a href="/wiki/20_Year_Update" title="20 Year Update">20 Year Update</a></i></li>
                   <li><i><a href="/wiki/Era_Digest:_Dark_Age" title="Era Digest: Dark Age">Era Digest: Dark Age</a></i></li>
                   <li><i><a href="/wiki/Era_Report:_2750" title="Era Report: 2750">Era Report: 2750</a></i></li>
                   <li><i><a href="/wiki/Era_Report:_3052" title="Era Report: 3052">Era Report: 3052</a></i></li>
                   <li><i><a href="/wiki/Era_Report:_3062" title="Era Report: 3062">Era Report: 3062</a></i></li>
                   <li><i><a href="/wiki/Era_Report:_3145" title="Era Report: 3145">Era Report: 3145</a></i></li>
                   <li><i><a href="/wiki/FedCom_Civil_War_(sourcebook)" title="FedCom Civil War (sourcebook)">FedCom Civil War</a></i></li>
                   <li><i><a href="/wiki/Field_Manual:_3085" title="Field Manual: 3085">Field Manual: 3085</a></i></li>
                   <li><i><a href="/wiki/Field_Manual:_3145" title="Field Manual: 3145">Field Manual: 3145</a></i></li>
                   <li><i><a href="/wiki/Field_Manual:_Warden_Clans" title="Field Manual: Warden Clans">Field Manual: Warden Clans</a></i></li>
                   <li><i><a href="/wiki/Field_Report:_LAAF" title="Field Report: LAAF">Field Report: LAAF</a></i></li>
                   <li><i><a href="/wiki/Handbook:_House_Steiner" title="Handbook: House Steiner">Handbook: House Steiner</a></i></li>
                   <li><i><a href="/wiki/Handbook:_Major_Periphery_States" title="Handbook: Major Periphery States">Handbook: Major Periphery States</a></i></li>
                   <li><i><a href="/wiki/Historical:_Liberation_of_Terra_Volume_1" title="Historical: Liberation of Terra Volume 1">Historical: Liberation of Terra Volume 1</a></i></li>
                   <li><i><a href="/wiki/Historical:_Reunification_War" title="Historical: Reunification War">Historical: Reunification War</a></i></li>
                   <li><i><a href="/wiki/Historical:_War_of_3039" title="Historical: War of 3039">Historical: War of 3039</a></i></li>
                   <li><i><a href="/wiki/House_Steiner_(The_Lyran_Commonwealth)" title="House Steiner (The Lyran Commonwealth)">House Steiner (The Lyran Commonwealth)</a></i></li>
                   <li><i><a href="/wiki/Jade_Falcon_Sourcebook" title="Jade Falcon Sourcebook">Jade Falcon Sourcebook</a></i></li>
                   <li><i><a href="/wiki/Jihad:_Final_Reckoning" title="Jihad: Final Reckoning">Jihad: Final Reckoning</a></i></li>
                   <li><i><a href="/wiki/Jihad_Secrets:_The_Blake_Documents" title="Jihad Secrets: The Blake Documents">Jihad Secrets: The Blake Documents</a></i></li>
                   <li><i><a href="/wiki/Masters_and_Minions:_The_StarCorps_Dossiers" title="Masters and Minions: The StarCorps Dossiers">Masters and Minions: The StarCorps Dossiers</a></i></li>
                   <li><i><a href="/wiki/Mercenaries_Supplemental" title="Mercenaries Supplemental">Mercenaries Supplemental</a></i></li>
                   <li><i><a href="/wiki/Objective_Raids" title="Objective Raids">Objective Raids</a></i></li>
                   <li><i><a href="/wiki/Objectives:_The_Clans" title="Objectives: The Clans">Objectives: The Clans</a></i></li>
                   <li><i><a href="/wiki/Recognition_Guide:_ilClan,_vol._8" title="Recognition Guide: ilClan, vol. 8">Recognition Guide: ilClan, vol. 8</a></i></li>
                   <li><i><a href="/wiki/Shattered_Fortress" title="Shattered Fortress">Shattered Fortress</a></i></li>
                   <li><i><a href="/wiki/Tamar_Rising" title="Tamar Rising">Tamar Rising</a></i></li></ul>
                   </div>
                   <!-- 
                   NewPP limit report
                   Cached time: 20240211051415
                   Cache expiry: 2592000
                   Dynamic content: false
                   Complications: []
                   [SMW] In‐text annotation parser time: 0 seconds
                   CPU time usage: 0.285 seconds
                   Real time usage: 0.684 seconds
                   Preprocessor visited node count: 1558/1000000
                   Preprocessor generated node count: 0/1000000
                   Post‐expand include size: 31686/2097152 bytes
                   Template argument size: 3113/2097152 bytes
                   Highest expansion depth: 10/40
                   Expensive parser function count: 0/200
                   Unstrip recursion depth: 0/20
                   Unstrip post‐expand size: 25240/5000000 bytes
                   Lua time usage: 0.240/7 seconds
                   Lua virtual size: 10.65 MB/50 MB
                   Lua estimated memory usage: 0 bytes
                   -->
                   <!--
                   Transclusion expansion time report (%,ms,calls,template)
                   100.00%  577.352      1 -total
                    55.53%  320.588      2 Template:Infobox
                    32.58%  188.074      1 Template:InfoBoxSystem
                    24.55%  141.724      1 Template:InfoBoxPlanet
                    20.84%  120.317      1 Template:Update_Needed
                    17.78%  102.635      1 Template:Ambox
                     5.41%   31.212      2 Template:Div_col
                     2.70%   15.572      2 Template:Str_number/trim
                     1.78%   10.257      1 Template:Columns-start
                     1.22%    7.042      1 Template:E
                   -->
                   
                   <!-- Saved in parser cache with key sarna_wiki-wiki_:pcache:idhash:1723-0!canonical and timestamp 20240211051421 and revision id 1052963
                    -->
                   </div><h2></h2></div><div class="printfooter">
                   Retrieved from "<a dir="ltr" href="https://www.sarna.net/wiki/index.php?title=Blackjack_(system)&amp;oldid=1052963">https://www.sarna.net/wiki/index.php?title=Blackjack_(system)&amp;oldid=1052963</a>"</div>
                               <div id="catlinks" class="catlinks" data-mw="interface"><div id="mw-normal-catlinks" class="mw-normal-catlinks"><a href="/wiki/Special:Categories" title="Special:Categories">Categories</a>: <ul><li><a href="/wiki/Category:Systems" title="Category:Systems">Systems</a></li><li><a href="/wiki/Category:Clan_Jade_Falcon_Systems" title="Category:Clan Jade Falcon Systems">Clan Jade Falcon Systems</a></li><li><a href="/wiki/Category:Clan_Steel_Viper_Systems" title="Category:Clan Steel Viper Systems">Clan Steel Viper Systems</a></li><li><a href="/wiki/Category:Lyran_Alliance_Systems" title="Category:Lyran Alliance Systems">Lyran Alliance Systems</a></li><li><a href="/wiki/Category:Lyran_Commonwealth_Systems" title="Category:Lyran Commonwealth Systems">Lyran Commonwealth Systems</a></li><li><a href="/wiki/Category:Federated_Commonwealth_Systems" title="Category:Federated Commonwealth Systems">Federated Commonwealth Systems</a></li></ul></div><div id="mw-hidden-catlinks" class="mw-hidden-catlinks mw-hidden-cats-hidden">Hidden category: <ul><li><a href="/wiki/Category:Articles_needing_updates" title="Category:Articles needing updates">Articles needing updates</a></li></ul></div></div>            <!-- end content -->
                                           <div class="visualClear"></div>
                           </div>
                      </div>
                   </div>
                   """;

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(25);
        actual.CountFactions(("No record")).Should().Be(1);
        actual.CountFactions(("Lyran Commonwealth")).Should().Be(8);
        actual.CountFactions(("Federated Commonwealth")).Should().Be(1);
        actual.CountFactions(("Clan Jade Falcon (from August)")).Should().Be(1);
        actual.CountFactions(("Clan Jade Falcon")).Should().Be(9);
        actual.CountFactions(("Clan Steel Viper")).Should().Be(2);
        actual.CountFactions(("Lyran Alliance (from December)")).Should().Be(1);
        actual.CountFactions("Lyran Alliance").Should().Be(1);
        actual.CountFactions("Clan Jade Falcon (from February)").Should().Be(1);
        actual.CountFactions("Alyina Mercantile League").Should().Be(1);
    }

    /// <summary>
    /// https://www.sarna.net/wiki/Kufstein
    /// </summary>
    [Fact]
    public void Should_ParseSystemKufstein()
    {
        var page = """
            <div id="content">
                    <a id="top"></a>
                
                        <script>window.sarnaAdProvider = 7;</script>
                    <script>
                    if (window.MutationObserver) {
                        var wrapper = document.getElementById('content');
                        var observer = new MutationObserver(function(mutations, observer) {
                            wrapper.style.height = '';
                            wrapper.style.minHeight = '';
                        });
                        observer.observe(wrapper, {
                            attributes: true,
                            attributeFilter: ['style']
                        });
                    }
                    </script>
                    <script data-cfasync="false" type="text/javascript">
                    var freestar = freestar || {};
                    freestar.queue = freestar.queue || [];
                    freestar.config = freestar.config || {};
                    freestar.config.enabled_slots = [];
                    </script>
                    <div id="p-banner-top-b" class="portlet">
                        <center>
                                                <script data-cfasync="false" type="text/javascript">
                                freestar.initCallback = function () { (freestar.config.enabled_slots.length === 0) ? freestar.initCallbackCalled = false : freestar.newAdSlots(freestar.config.enabled_slots) }
                                </script>
                                <script src="https://a.pub.network/sarna-net/pubfig.min.js" async=""></script>
                                <div align="center" data-freestar-ad="__728x90" id="sarna_leaderboard_atf">
                                    <script data-cfasync="false" type="text/javascript">
                                    freestar.config.enabled_slots.push({ placementName: "sarna_leaderboard_atf", slotId: "sarna_leaderboard_atf" });
                                    </script>
                                </div>
                                <!-- Quantcast Choice. Consent Manager Tag v2.0 (for TCF 2.0) -->
                                <script type="text/javascript" async="true">
                                (function initQuantcast() {
                                    !function(){var e=document.createElement("script"),t=document.getElementsByTagName("script")[0],a="https://cmp.quantcast.com".concat("/choice/","kevTBasPvBb4b","/","sarna.net","/choice.js?tag_version=V2"),n=0;e.async=!0,e.type="text/javascript",e.src=a,t.parentNode.insertBefore(e,t),function(){for(var e,t="__tcfapiLocator",a=[],n=window;n;){try{if(n.frames[t]){e=n;break}}catch(e){}if(n===window.top)break;n=n.parent}e||(function e(){var a=n.document,i=!!n.frames[t];if(!i)if(a.body){var s=a.createElement("iframe");s.style.cssText="display:none",s.name=t,a.body.appendChild(s)}else setTimeout(e,5);return!i}(),n.__tcfapi=function(){var e,t=arguments;if(!t.length)return a;if("setGdprApplies"===t[0])t.length>3&&2===t[2]&&"boolean"==typeof t[3]&&(e=t[3],"function"==typeof t[2]&&t[2]("set",!0));else if("ping"===t[0]){var n={gdprApplies:e,cmpLoaded:!1,cmpStatus:"stub"};"function"==typeof t[2]&&t[2](n)}else"init"===t[0]&&"object"==typeof t[3]&&(t[3]=Object.assign(t[3],{tag_version:"V2"})),a.push(t)},n.addEventListener("message",function(e){var t="string"==typeof e.data,a={};try{a=t?JSON.parse(e.data):e.data}catch(e){}var n=a.__tcfapiCall;n&&window.__tcfapi(n.command,n.version,function(a,i){var s={__tcfapiReturn:{returnValue:a,success:i,callId:n.callId}};t&&(s=JSON.stringify(s)),e&&e.source&&e.source.postMessage&&e.source.postMessage(s,"*")},n.parameter)},!1))}();var i=function(){var e=arguments;typeof window.__uspapi!==i&&setTimeout(function(){void 0!==window.__uspapi&&window.__uspapi.apply(window.__uspapi,e)},500)};if(void 0===window.__uspapi){window.__uspapi=i;var s=setInterval(function(){n++,window.__uspapi===i&&n<3?console.warn("USP is not accessible"):clearInterval(s)},6e3)}}();
                                })();
                                </script>
                                            </center>
                    </div>
                            <div id="socialIcons">
                        <div class="share-bar">
                            <div class="share-bar-service share-bar-service-facebook">
                                <a class="social-share-frame" href="https://www.facebook.com/dialog/share?app_id=165706373584987&amp;display=popup&amp;href=https://www.sarna.net//wiki/Kufstein">
                                    <i class="fab fa-facebook-f"></i>
                                    <span class="share-bar-service-name">Like</span>
                                </a>
                            </div>
                            <div class="share-bar-service share-bar-service-twitter">
                                <a class="social-share-frame" href="https://twitter.com/share?url=https://www.sarna.net//wiki/Kufstein&amp;text=Kufstein">
                                    <i class="fab fa-twitter"></i>
                                    <span class="share-bar-service-name">Tweet</span>
                                </a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                        <h1 id="firstHeading" class="firstHeading">Kufstein</h1>
                        <div id="right-column">
                                        <div id="right-ad-1" class="ad-container" style="width: 300px; height: 600px; position: sticky; top: 0">
                                <div align="center" data-freestar-ad="__300x600" id="sarna_rightrail_sticky">
                                    <script data-cfasync="false" type="text/javascript">
                                    freestar.config.enabled_slots.push({ placementName: "sarna_rightrail_sticky", slotId: "sarna_rightrail_sticky" });
                                    </script>
                                </div>
                            </div>
                            
                        <div style="width: 300px; margin-top: 10px" class="portlet" id="right-news">
                            <h5>Sarna News</h5>
                                                    <a href="https://www.sarna.net/news/your-battletech-news-round-up-for-january-2024/">
                                        <img height="154" width="300" src="//cfw.sarna.net/news/wp-content/uploads/2024/01/Nightstar-Oil-Painting-by-1001WingedHussars.jpeg" style="max-width: 280px; height: auto; margin: 5px 10px" alt="Sarna.net News: Your BattleTech News Round-Up For January, 2024">
                                    </a>
                                
                            <!-- mailing list signup form -->
                            <form action="//sarna.us17.list-manage.com/subscribe/post?u=56a7c808c4fc6faa708b0e488&amp;id=bd6a480de0" method="post" id="mc-embedded-subscribe-form" name="mc-embedded-subscribe-form" class="validate" target="_blank">
                                <input type="text" value="" name="EMAIL" class="required email" placeholder="email" autocomplete="email" id="mce-EMAIL" style="width: 210px">
                                <input type="submit" value="Sub!" name="subscribe" id="mc-embedded-subscribe" class="btn" style="width: 75px">
                            </form>
            
                            <ul>
                                                <li><a href="https://www.sarna.net/news/your-battletech-news-round-up-for-january-2024/" rel="bookmark">
                                    Your BattleTech News Round-Up For January, 2024                    </a></li>
                                                    <li><a href="https://www.sarna.net/news/battletech-in-2024-an-interview-with-line-developer-ray-arrastia-assistant-line-developer-aaron-cahall/" rel="bookmark">
                                    BattleTech In 2024 - An Interview With Line Developer Ray Arrastia &amp; Assistant Line Developer Aaron Cahall                    </a></li>
                                                    <li><a href="https://www.sarna.net/news/bad-mechs-hellfire/" rel="bookmark">
                                    Bad ‘Mechs - Hellfire                    </a></li>
                                                    <li><a href="https://www.sarna.net/news/sarnas-50000-article-celebration/" rel="bookmark">
                                    Sarna's 50,000 Article Celebration!                    </a></li>
                                                    <li><a href="https://www.sarna.net/news/your-battletech-news-round-up-for-december-2023/" rel="bookmark">
                                    Your BattleTech News Round-Up For December, 2023                    </a></li>
                                                    <li><a href="/news/">Read more →</a>
                            </li></ul>
                        </div>
            
                                                            
                        <div id="right-self-ad" class="right-ad" style="margin-top: 5px; width: 300px; height: 250px; text-align: center">
                            <a href="https://www.sarna.net/wiki/BattleTechWiki:Support#Sarna_Merch"><img src="//cfw.sarna.net/images/ads/amazon-merch-sarna-white-hoodie-250x234.jpg" height="234" width="250" alt="Sarna.net Merch!"></a>
                            <p><a href="https://www.sarna.net/wiki/BattleTechWiki:Support#Sarna_Merch">Support sarna.net!  Merch!</a></p>
                        </div>
            
                        <div id="right-community-ad" class="right-ad" style="margin-top: 5px; width: 300px; height: 350px; text-align: center">
                            <h3><a href="https://www.sarna.net/wiki/BattleTechWiki:CommunityAds">BattleTech Community Ads</a></h3>
            
                                                <a href="https://www.bta3062.com/"><img src="//cfw.sarna.net/images/ads/bta.png" width="300" height="300" alt="BattleTech Advanced 3062"></a>
                                <p><a href="https://www.bta3062.com/">BattleTech Advanced 3062</a></p>
                                            </div>
            
                                                                    </div>
                        <div id="bodyContent" style="margin-right: 310px">
                    <div>
                        <div id="contentSub"></div>
                                <!-- start content -->
                <div id="mw-content-text" lang="en" dir="ltr" class="mw-content-ltr"><div class="mw-notification-area mw-notification-area-floating" id="mw-notification-area" style="display: none;"></div><div class="mw-parser-output"><style data-mw-deduplicate="TemplateStyles:r1004727">.mw-parser-output .infobox-subbox{padding:0;border:none;margin:-3px;width:auto;min-width:100%;font-size:100%;clear:none;float:none;background-color:transparent}.mw-parser-output .infobox-3cols-child{margin:auto}.mw-parser-output .infobox .navbar{font-size:100%}body.skin-minerva .mw-parser-output .infobox-header,body.skin-minerva .mw-parser-output .infobox-subheader,body.skin-minerva .mw-parser-output .infobox-above,body.skin-minerva .mw-parser-output .infobox-title,body.skin-minerva .mw-parser-output .infobox-image,body.skin-minerva .mw-parser-output .infobox-full-data,body.skin-minerva .mw-parser-output .infobox-below{text-align:center}</style><table class="infobox"><tbody><tr><th colspan="2" class="infobox-above" style="background:#c5d07d; border:0.15em solid #222; padding:0.2em;">Kufstein</th></tr><tr><td colspan="2" class="infobox-subheader" style="border:0.15em solid #222; padding:0.2em;"><div class="infoboxnavlink"><div class="system-nav-left">← 3135</div> <div class="system-nav-current">3151</div> <div class="system-nav-right"></div></div></td></tr><tr><td colspan="2" class="infobox-image" style="border:0.15em solid #222; padding:0.2em;"><div class="center"><div class="floatnone"><a href="/wiki/File:Kufstein_3151.svg" class="image"><img alt="Kufstein 3151.svg" src="https://cfw.sarna.net/wiki/images/1/1d/Kufstein_3151.svg?timestamp=20210827184901" width="240" height="274"></a></div></div><div class="infobox-caption">Kufstein <a href="#Nearby_Systems">nearby systems</a><br>(<a href="/wiki/BattleTechWiki:Map_Legend" title="BattleTechWiki:Map Legend">Map Legend</a>)</div></td></tr><tr><th colspan="2" class="infobox-header" style="background:#607D8B; color:#FFF; border:0.15em solid #333; padding:0.1em;">System Information</th></tr><tr><th scope="row" class="infobox-label" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">X:Y Coordinates</th><td class="infobox-data" style="font-size:14px; font-weight:400; line-height: 1.4; text-align: left; border:0.15em solid #222; padding:0.2em;">-11.125&nbsp;: 341.426<sup class="noprint" style="white-space:nowrap">[<i><a href="/wiki/BattleTechWiki:System_coordinates" title="BattleTechWiki:System coordinates">e</a></i>]</sup></td></tr></tbody></table>
            <div id="toc" class="toc"><input type="checkbox" role="button" id="toctogglecheckbox" class="toctogglecheckbox" style="display:none"><div class="toctitle" lang="en" dir="ltr"><h2>Contents</h2><span class="toctogglespan"><label class="toctogglelabel" for="toctogglecheckbox"></label></span></div>
            <ul>
            <li class="toclevel-1 tocsection-1"><a href="#Political_Affiliation"><span class="tocnumber">1</span> <span class="toctext">Political Affiliation</span></a></li>
            <li class="toclevel-1 tocsection-2"><a href="#Kufstein"><span class="tocnumber">2</span> <span class="toctext">Kufstein</span></a>
            <ul>
            <li class="toclevel-2 tocsection-3"><a href="#Planetary_History"><span class="tocnumber">2.1</span> <span class="toctext">Planetary History</span></a>
            <ul>
            <li class="toclevel-3 tocsection-4"><a href="#Early_History"><span class="tocnumber">2.1.1</span> <span class="toctext">Early History</span></a></li>
            <li class="toclevel-3 tocsection-5"><a href="#First_Succession_War"><span class="tocnumber">2.1.2</span> <span class="toctext">First Succession War</span></a></li>
            <li class="toclevel-3 tocsection-6"><a href="#Fourth_Succession_War"><span class="tocnumber">2.1.3</span> <span class="toctext">Fourth Succession War</span></a></li>
            <li class="toclevel-3 tocsection-7"><a href="#Clan_Invasion"><span class="tocnumber">2.1.4</span> <span class="toctext">Clan Invasion</span></a></li>
            </ul>
            </li>
            <li class="toclevel-2 tocsection-8"><a href="#Military_Deployment"><span class="tocnumber">2.2</span> <span class="toctext">Military Deployment</span></a>
            <ul>
            <li class="toclevel-3 tocsection-9"><a href="#2765"><span class="tocnumber">2.2.1</span> <span class="toctext">2765</span></a></li>
            <li class="toclevel-3 tocsection-10"><a href="#2786"><span class="tocnumber">2.2.2</span> <span class="toctext">2786</span></a></li>
            <li class="toclevel-3 tocsection-11"><a href="#2821"><span class="tocnumber">2.2.3</span> <span class="toctext">2821</span></a></li>
            <li class="toclevel-3 tocsection-12"><a href="#3050"><span class="tocnumber">2.2.4</span> <span class="toctext">3050</span></a></li>
            <li class="toclevel-3 tocsection-13"><a href="#3145"><span class="tocnumber">2.2.5</span> <span class="toctext">3145</span></a></li>
            </ul>
            </li>
            <li class="toclevel-2 tocsection-14"><a href="#Planetary_Locations"><span class="tocnumber">2.3</span> <span class="toctext">Planetary Locations</span></a></li>
            </ul>
            </li>
            <li class="toclevel-1 tocsection-15"><a href="#Map_Gallery"><span class="tocnumber">3</span> <span class="toctext">Map Gallery</span></a></li>
            <li class="toclevel-1 tocsection-16"><a href="#Nearby_Systems"><span class="tocnumber">4</span> <span class="toctext">Nearby Systems</span></a></li>
            <li class="toclevel-1 tocsection-17"><a href="#References"><span class="tocnumber">5</span> <span class="toctext">References</span></a></li>
            <li class="toclevel-1 tocsection-18"><a href="#Bibliography"><span class="tocnumber">6</span> <span class="toctext">Bibliography</span></a></li>
            </ul>
            </div>
            
            <div class="sarna_inline_mobile_ad"><div id="nn_mobile_mpu1"></div><div align="center" data-freestar-ad="__300x250" id="sarna_mobile_incontent_1"><script data-cfasync="false" type="text/javascript">if(window.freestar){freestar.config.enabled_slots.push({ placementName: 'sarna_mobile_incontent_1', slotId: 'sarna_mobile_incontent_1' });}</script></div></div><h2><span class="mw-headline" id="Political_Affiliation">Political Affiliation</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=1" title="Edit section: Political Affiliation">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <style data-mw-deduplicate="TemplateStyles:r983948">.mw-parser-output .div-col{margin-top:0.3em;column-width:30em}.mw-parser-output .div-col-small{font-size:90%}.mw-parser-output .div-col-rules{column-rule:1px solid #aaa}.mw-parser-output .div-col dl,.mw-parser-output .div-col ol,.mw-parser-output .div-col ul{margin-top:0}.mw-parser-output .div-col li,.mw-parser-output .div-col dd{page-break-inside:avoid;break-inside:avoid-column}.mw-parser-output .plainlist ol,.mw-parser-output .plainlist ul{line-height:inherit;list-style:none;margin:0}.mw-parser-output .plainlist ol li,.mw-parser-output .plainlist ul li{margin-bottom:0}</style><div class="div-col" style="column-width: 25em;">
            <ul><li><a href="/wiki/2319" title="2319">2319</a> - No record<sup id="cite_ref-HB:HKp18_1-0" class="reference"><a href="#cite_note-HB:HKp18-1">[1]</a></sup></li>
            <li>ca. <a href="/wiki/2330" title="2330">2330</a>-<a href="/wiki/2340" title="2340">2340</a> - <a href="/wiki/Principality_of_Rasalhague" title="Principality of Rasalhague">Principality of Rasalhague</a><sup id="cite_ref-TP1Ep17_2-0" class="reference"><a href="#cite_note-TP1Ep17-2">[2]</a></sup></li>
            <li><a href="/wiki/2571" title="2571">2571</a> - No record<sup id="cite_ref-HB:HKp31_3-0" class="reference"><a href="#cite_note-HB:HKp31-3">[3]</a></sup><sup id="cite_ref-4" class="reference"><a href="#cite_note-4">[4]</a></sup></li>
            <li><a href="/wiki/2596" title="2596">2596</a> - No record<sup id="cite_ref-HRWp159_5-0" class="reference"><a href="#cite_note-HRWp159-5">[5]</a></sup></li>
            <li><a href="/wiki/2750" title="2750">2750</a> - <a href="/wiki/Lyran_Commonwealth" title="Lyran Commonwealth">Lyran Commonwealth</a><sup id="cite_ref-ER:2750p37_6-0" class="reference"><a href="#cite_note-ER:2750p37-6">[6]</a></sup></li>
            <li><a href="/wiki/2764" title="2764">2764</a> - Lyran Commonwealth<sup id="cite_ref-7" class="reference"><a href="#cite_note-7">[7]</a></sup></li>
            <li><a href="/wiki/2765" title="2765">2765</a> - Lyran Commonwealth<sup id="cite_ref-H:LoTv1p11_8-0" class="reference"><a href="#cite_note-H:LoTv1p11-8">[8]</a></sup></li>
            <li><a href="/wiki/2786" title="2786">2786</a> - Lyran Commonwealth<sup id="cite_ref-FSW.28SB.29p25_9-0" class="reference"><a href="#cite_note-FSW.28SB.29p25-9">[9]</a></sup></li>
            <li><a href="/wiki/2822" title="2822">2822</a> - <a href="/wiki/Draconis_Combine" title="Draconis Combine">Draconis Combine</a><sup id="cite_ref-HB:HKp43_10-0" class="reference"><a href="#cite_note-HB:HKp43-10">[10]</a></sup><sup id="cite_ref-11" class="reference"><a href="#cite_note-11">[11]</a></sup><sup id="cite_ref-H:LOTV2p122_12-0" class="reference"><a href="#cite_note-H:LOTV2p122-12">[12]</a></sup><sup id="cite_ref-FSW.28SB.29p113_13-0" class="reference"><a href="#cite_note-FSW.28SB.29p113-13">[13]</a></sup></li>
            <li><a href="/wiki/2864" title="2864">2864</a> - Draconis Combine<sup id="cite_ref-HB:HKp53_14-0" class="reference"><a href="#cite_note-HB:HKp53-14">[14]</a></sup><sup id="cite_ref-15" class="reference"><a href="#cite_note-15">[15]</a></sup></li>
            <li><a href="/wiki/3025" title="3025">3025</a> - Draconis Combine<sup id="cite_ref-16" class="reference"><a href="#cite_note-16">[16]</a></sup><sup id="cite_ref-HB:HKp64_17-0" class="reference"><a href="#cite_note-HB:HKp64-17">[17]</a></sup><sup id="cite_ref-18" class="reference"><a href="#cite_note-18">[18]</a></sup><sup id="cite_ref-HB:HSp47_19-0" class="reference"><a href="#cite_note-HB:HSp47-19">[19]</a></sup></li>
            <li><a href="/wiki/3029" title="3029">3029</a> - Draconis Combine (January)<sup id="cite_ref-FSWMAv2p60_20-0" class="reference"><a href="#cite_note-FSWMAv2p60-20">[20]</a></sup> / Lyran Commonwealth<sup id="cite_ref-FSWMAv2p61_21-0" class="reference"><a href="#cite_note-FSWMAv2p61-21">[21]</a></sup></li>
            <li><a href="/wiki/3030" title="3030">3030</a> - Lyran Commonwealth (January)<sup id="cite_ref-FSWMAv2p61_21-1" class="reference"><a href="#cite_note-FSWMAv2p61-21">[21]</a></sup> / Draconis Combine<sup id="cite_ref-FSWMAv2p93_22-0" class="reference"><a href="#cite_note-FSWMAv2p93-22">[22]</a></sup><sup id="cite_ref-23" class="reference"><a href="#cite_note-23">[23]</a></sup><sup id="cite_ref-HB:HKp66_24-0" class="reference"><a href="#cite_note-HB:HKp66-24">[24]</a></sup></li>
            <li><a href="/wiki/3034" title="3034">3034</a> - <a href="/wiki/Free_Rasalhague_Republic" title="Free Rasalhague Republic">Free Rasalhague Republic</a><sup id="cite_ref-H:BWp103_25-0" class="reference"><a href="#cite_note-H:BWp103-25">[25]</a></sup></li>
            <li><a href="/wiki/3040" title="3040">3040</a> - Free Rasalhague Republic<sup id="cite_ref-26" class="reference"><a href="#cite_note-26">[26]</a></sup><sup id="cite_ref-HB:HKp68_27-0" class="reference"><a href="#cite_note-HB:HKp68-27">[27]</a></sup><sup id="cite_ref-28" class="reference"><a href="#cite_note-28">[28]</a></sup></li>
            <li><a href="/wiki/3050" title="3050">3050</a> - Free Rasalhague Republic<sup id="cite_ref-ER3052p11_29-0" class="reference"><a href="#cite_note-ER3052p11-29">[29]</a></sup></li>
            <li><a href="/wiki/3052" title="3052">3052</a> - <a href="/wiki/Clan_Wolf" title="Clan Wolf">Clan Wolf</a><sup id="cite_ref-ER3052p23_30-0" class="reference"><a href="#cite_note-ER3052p23-30">[30]</a></sup><sup id="cite_ref-31" class="reference"><a href="#cite_note-31">[31]</a></sup><sup id="cite_ref-HB:HKp71_32-0" class="reference"><a href="#cite_note-HB:HKp71-32">[32]</a></sup></li>
            <li><a href="/wiki/3057" title="3057">3057</a> - Clan Wolf<sup id="cite_ref-33" class="reference"><a href="#cite_note-33">[33]</a></sup></li>
            <li><a href="/wiki/3063" title="3063">3063</a> - Clan Wolf<sup id="cite_ref-34" class="reference"><a href="#cite_note-34">[34]</a></sup></li>
            <li><a href="/wiki/3067" title="3067">3067</a> - Clan Wolf<sup id="cite_ref-HB:HKp74_35-0" class="reference"><a href="#cite_note-HB:HKp74-35">[35]</a></sup><sup id="cite_ref-36" class="reference"><a href="#cite_note-36">[36]</a></sup><sup id="cite_ref-HB:HSp70_37-0" class="reference"><a href="#cite_note-HB:HSp70-37">[37]</a></sup></li>
            <li><a href="/wiki/3075" title="3075">3075</a> - Clan Wolf<sup id="cite_ref-38" class="reference"><a href="#cite_note-38">[38]</a></sup></li>
            <li><a href="/wiki/3079" title="3079">3079</a> - Clan Wolf<sup id="cite_ref-FR:Cp27_39-0" class="reference"><a href="#cite_note-FR:Cp27-39">[39]</a></sup><sup id="cite_ref-FR:DCMSp21_40-0" class="reference"><a href="#cite_note-FR:DCMSp21-40">[40]</a></sup></li>
            <li><a href="/wiki/3081" title="3081">3081</a> - Clan Wolf<sup id="cite_ref-41" class="reference"><a href="#cite_note-41">[41]</a></sup></li>
            <li><a href="/wiki/3085" title="3085">3085</a> - Clan Wolf<sup id="cite_ref-42" class="reference"><a href="#cite_note-42">[42]</a></sup></li>
            <li><a href="/wiki/3135" title="3135">3135</a> - Clan Wolf<sup id="cite_ref-ER:3145p11_43-0" class="reference"><a href="#cite_note-ER:3145p11-43">[43]</a></sup></li>
            <li><a href="/wiki/3145" title="3145">3145</a> - <a href="/wiki/Rasalhague_Dominion" title="Rasalhague Dominion">Rasalhague Dominion</a><sup id="cite_ref-ER:3145p39_44-0" class="reference"><a href="#cite_note-ER:3145p39-44">[44]</a></sup><sup id="cite_ref-FM:3145pVI_45-0" class="reference"><a href="#cite_note-FM:3145pVI-45">[45]</a></sup></li>
            <li><a href="/wiki/3151" title="3151">3151</a> - Rasalhague Dominion<sup id="cite_ref-SFp102-103_46-0" class="reference"><a href="#cite_note-SFp102-103-46">[46]</a></sup></li></ul>
            </div>
            <hr>
            <h2><span class="mw-headline" id="Kufstein">Kufstein</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=2" title="Edit section: Kufstein">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <link rel="mw-deduplicated-inline-style" href="mw-data:TemplateStyles:r1004727"><table class="infobox"><tbody><tr><th colspan="2" class="infobox-above" style="background:#c5d07d; border:0.15em solid #222; padding:0.2em;">Kufstein</th></tr></tbody></table>
            <h3><span class="mw-headline" id="Planetary_History">Planetary History</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=3" title="Edit section: Planetary History">edit</a><span class="mw-editsection-bracket">]</span></span></h3>
            <h4><span class="mw-headline" id="Early_History">Early History</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=4" title="Edit section: Early History">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <p>When <a href="/wiki/Shiro_Kurita" title="Shiro Kurita">Shiro Kurita</a> launched his invasion of the <a href="/wiki/Principality_of_Rasalhague" title="Principality of Rasalhague">Principality of Rasalhague</a> in an attempt to annex the Principality and add it to the <a href="/wiki/Draconis_Combine" title="Draconis Combine">Draconis Combine</a> during the 2330s and 2340s the brutalities inflicted by the Combine forces were such that many of the survivors fled from the Principality. In some instances, survivors from one or more Principality worlds banded together to found new colonies. Craftsmen and artists from both <b>Kufstein</b> and <a href="/wiki/Predlitz" title="Predlitz">Predlitz</a> joined with technicians from <a href="/wiki/St._John" title="St. John">St. John</a> and engineers from <a href="/wiki/New_Bergen" title="New Bergen">New Bergen</a> to settle the worlds of <a href="/wiki/Bensinger" title="Bensinger">Bensinger</a>, <a href="/wiki/Steelton" title="Steelton">Steelton</a> and <a href="/wiki/Toland" title="Toland">Toland</a>. All three worlds were on the outer reaches of settled human space, cold and icy worlds that would subsequently be courted by the growing <a href="/wiki/Rim_Worlds_Republic" title="Rim Worlds Republic">Rim Worlds Republic</a>.<sup id="cite_ref-TP1Ep17_2-1" class="reference"><a href="#cite_note-TP1Ep17-2">[2]</a></sup>
            </p>
            <h4><span class="mw-headline" id="First_Succession_War">First Succession War</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=5" title="Edit section: First Succession War">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <p>Kufstein was one of a number of strategically important systems in the Lyran Commonwealth to be subjected to bombardment by the Draconis Combine between <a href="/wiki/2786" title="2786">2786</a> and <a href="/wiki/2790" title="2790">2790</a>, as the <a href="/wiki/Lyran_Commonwealth_Armed_Forces" title="Lyran Commonwealth Armed Forces">Lyran Commonwealth Armed Forces</a> began to crack under the pressure of continuing efforts to annex the <a href="/wiki/Bolan_Thumb" title="Bolan Thumb">Bolan Thumb</a> while also attempting to maintain a defensive posture against both the <a href="/wiki/Draconis_Combine_Mustered_Soldiery" title="Draconis Combine Mustered Soldiery">Draconis Combine Mustered Soldiery</a> and the <a href="/wiki/Free_Worlds_League_Military" title="Free Worlds League Military">Free Worlds League Military</a>.<sup id="cite_ref-FSWp62_47-0" class="reference"><a href="#cite_note-FSWp62-47">[47]</a></sup>
            </p>
            <h4><span class="mw-headline" id="Fourth_Succession_War">Fourth Succession War</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=6" title="Edit section: Fourth Succession War">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <p>Kufstein was conquered by the Lyran Commonwealth Lyran following a planetary assault during <a href="/wiki/Operation_G%C3%96TTERD%C3%84MMERUNG" title="Operation GÖTTERDÄMMERUNG">Operation GÖTTERDÄMMERUNG</a>, the major Lyran Commonwealth offensive of the <a href="/wiki/Fourth_Succession_War" title="Fourth Succession War">Fourth Succession War</a>. As a part of the final wave of Operation GÖTTERDÄMMERUNG the <a href="/wiki/Lyran_Commonwealth_Armed_Forces" title="Lyran Commonwealth Armed Forces">Lyran Commonwealth Armed Forces</a> deployed the <a href="/wiki/Blackhearts" title="Blackhearts">Blackhearts</a> to invade Kufstein in late <a href="/wiki/3028" title="3028">3028</a>;<sup id="cite_ref-NAIS4SWp44_48-0" class="reference"><a href="#cite_note-NAIS4SWp44-48">[48]</a></sup> the Combine was still effectively in control of Kufstein in January <a href="/wiki/3029" title="3029">3029</a>,<sup id="cite_ref-FSWMAv2p60_20-1" class="reference"><a href="#cite_note-FSWMAv2p60-20">[20]</a></sup> but during 3029 the Commonwealth took control of the planet. The Commonwealth retained control of Kufstein until at least January 3030, by which point the Lyran Commonwealth had ended the offensive actions launched as a part of <a href="/wiki/Operation_HOLDUR" title="Operation HOLDUR">Operation HOLDUR</a> and was consolidating its gains.<sup id="cite_ref-FSWMAv2p61_21-2" class="reference"><a href="#cite_note-FSWMAv2p61-21">[21]</a></sup> <a href="/wiki/Coordinator" class="mw-redirect" title="Coordinator">Coordinator</a> <a href="/wiki/Takashi_Kurita" title="Takashi Kurita">Takashi Kurita</a> ordered the <a href="/wiki/DCMS" class="mw-redirect" title="DCMS">DCMS</a> to halt all but the most promising attacks and assume a defensive posture; by the time the war was officially ended via a peace treaty mediated by <a href="/wiki/ComStar" title="ComStar">ComStar</a><sup id="cite_ref-FSWMAv1p92_49-0" class="reference"><a href="#cite_note-FSWMAv1p92-49">[49]</a></sup> Kufstein had been returned to Combine control,<sup id="cite_ref-FSWMAv2p93_22-1" class="reference"><a href="#cite_note-FSWMAv2p93-22">[22]</a></sup> most likely either via a counterattack or as a part of the peace negotiations.
            </p>
            <h4><span class="mw-headline" id="Clan_Invasion">Clan Invasion</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=7" title="Edit section: Clan Invasion">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <p>In August <a href="/wiki/3050" title="3050">3050</a>, as part of the Fourth Wave of <a href="/wiki/Operation_Revival" class="mw-redirect" title="Operation Revival">Operation Revival</a>, Kufstein was invaded by <a href="/wiki/Trinary" title="Trinary">Trinaries</a> First and Second Striker of <a href="/wiki/Clan_Wolf" title="Clan Wolf">Clan Wolf</a>'s <a href="/wiki/11th_Wolf_Guards_(Clan_Wolf)" class="mw-redirect" title="11th Wolf Guards (Clan Wolf)">11th Wolf Guards</a>.  They were opposed by the 1st and 4th Kufstein Planetary Guard Brigades, which fought the invaders at Inston Beck, Nalley Valley and the Crow River; the extreme southern latitudes of these battle sites meant all of the fighting took place in nighttime conditions.  The defenders were particularly ferocious during the battle, especially the all-woman 2nd Kufstein Mechanized Regiment, but remarkably the Wolf warriors took only light casualties during the seven-day conquest of the planet.<sup id="cite_ref-50" class="reference"><a href="#cite_note-50">[50]</a></sup>
            </p>
            <h3><span class="mw-headline" id="Military_Deployment">Military Deployment</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=8" title="Edit section: Military Deployment">edit</a><span class="mw-editsection-bracket">]</span></span></h3>
            <h4><span class="mw-headline" id="2765">2765</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=9" title="Edit section: 2765">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <ul><li><a href="/wiki/Third_Arcturan_Guards" class="mw-redirect" title="Third Arcturan Guards">Third Arcturan Guards</a><sup id="cite_ref-FM2765:LCp9_51-0" class="reference"><a href="#cite_note-FM2765:LCp9-51">[51]</a></sup></li></ul>
            <h4><span class="mw-headline" id="2786">2786</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=10" title="Edit section: 2786">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <ul><li>Third Arcturan Guards<sup id="cite_ref-FSWp139_52-0" class="reference"><a href="#cite_note-FSWp139-52">[52]</a></sup></li></ul>
            <h4><span class="mw-headline" id="2821">2821</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=11" title="Edit section: 2821">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <ul><li><a href="/wiki/6th_Rasalhague_Regulars" title="6th Rasalhague Regulars">Sixth Rasalhague Regulars</a><sup id="cite_ref-FSWp137_53-0" class="reference"><a href="#cite_note-FSWp137-53">[53]</a></sup></li></ul>
            <h4><span class="mw-headline" id="3050">3050</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=12" title="Edit section: 3050">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <ul><li><a href="/wiki/Kufstein_Militia" title="Kufstein Militia">First Kufstein Planetary Guard Brigade</a><sup id="cite_ref-WCS062_54-0" class="reference"><a href="#cite_note-WCS062-54">[54]</a></sup></li>
            <li><a href="/wiki/Kufstein_Militia" title="Kufstein Militia">Fourth Kufstein Planetary Guard Brigade</a><sup id="cite_ref-WCS062_54-1" class="reference"><a href="#cite_note-WCS062-54">[54]</a></sup></li></ul>
            <h4><span class="mw-headline" id="3145">3145</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=13" title="Edit section: 3145">edit</a><span class="mw-editsection-bracket">]</span></span></h4>
            <ul><li><a href="/wiki/1st_Rasalhague_Bears_(Clan_Ghost_Bear)" class="mw-redirect" title="1st Rasalhague Bears (Clan Ghost Bear)">First Rasalhague Bears</a><sup id="cite_ref-FM:3145p172_55-0" class="reference"><a href="#cite_note-FM:3145p172-55">[55]</a></sup></li></ul>
            <h3><span class="mw-headline" id="Planetary_Locations">Planetary Locations</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=14" title="Edit section: Planetary Locations">edit</a><span class="mw-editsection-bracket">]</span></span></h3>
            <ul><li>Crow River: The site of a major battle during <a href="/wiki/Operation_REVIVAL" class="mw-redirect" title="Operation REVIVAL">Operation REVIVAL</a>.<sup id="cite_ref-WCS062_54-2" class="reference"><a href="#cite_note-WCS062-54">[54]</a></sup></li>
            <li>Inston Beck: The site of a major battle during <a href="/wiki/Operation_REVIVAL" class="mw-redirect" title="Operation REVIVAL">Operation REVIVAL</a>.<sup id="cite_ref-WCS062_54-3" class="reference"><a href="#cite_note-WCS062-54">[54]</a></sup></li>
            <li>Nalley Valley: The site of a major battle during <a href="/wiki/Operation_REVIVAL" class="mw-redirect" title="Operation REVIVAL">Operation REVIVAL</a>.<sup id="cite_ref-WCS062_54-4" class="reference"><a href="#cite_note-WCS062-54">[54]</a></sup></li></ul>
            <div class="sarna_inline_mobile_ad"><div id="nn_mobile_mpu2"></div><div align="center" data-freestar-ad="__300x250" id="sarna_mobile_incontent_2"><script data-cfasync="false" type="text/javascript">if(window.freestar){freestar.config.enabled_slots.push({ placementName: 'sarna_mobile_incontent_2', slotId: 'sarna_mobile_incontent_2' });}</script></div></div><h2><span class="mw-headline" id="Map_Gallery">Map Gallery</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=15" title="Edit section: Map Gallery">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <div class="system-map-gallery"><div class="system-map-gallery-years"><div class="system-map-gallery-year-header">Years:</div><div class="system-map-gallery-year" data-year="2571">2571</div><div class="system-map-gallery-year" data-year="2596">2596</div><div class="system-map-gallery-year" data-year="2750">2750</div><div class="system-map-gallery-year" data-year="2765">2765</div><div class="system-map-gallery-year" data-year="2767">2767</div><div class="system-map-gallery-year" data-year="2783">2783</div><div class="system-map-gallery-year" data-year="2786">2786</div><div class="system-map-gallery-year" data-year="2821">2821</div><div class="system-map-gallery-year" data-year="2822">2822</div><div class="system-map-gallery-year" data-year="2830">2830</div><div class="system-map-gallery-year" data-year="2864">2864</div><div class="system-map-gallery-year" data-year="3025">3025</div><div class="system-map-gallery-year" data-year="3030">3030</div><div class="system-map-gallery-year" data-year="3040">3040</div><div class="system-map-gallery-year" data-year="3049">3049</div><div class="system-map-gallery-year" data-year="3052">3052</div><div class="system-map-gallery-year" data-year="3057">3057</div><div class="system-map-gallery-year" data-year="3058">3058</div><div class="system-map-gallery-year" data-year="3059">3059</div><div class="system-map-gallery-year" data-year="3063">3063</div><div class="system-map-gallery-year" data-year="3067">3067</div><div class="system-map-gallery-year" data-year="3068">3068</div><div class="system-map-gallery-year" data-year="3075">3075</div><div class="system-map-gallery-year" data-year="3081">3081</div><div class="system-map-gallery-year" data-year="3085">3085</div><div class="system-map-gallery-year" data-year="3095">3095</div><div class="system-map-gallery-year" data-year="3130">3130</div><div class="system-map-gallery-year" data-year="3135">3135</div><div class="system-map-gallery-year" data-year="3145">3145</div><div class="system-map-gallery-year selected" data-year="3151">3151</div></div><div class="system-map-gallery-images-container"><div class="system-map-gallery-images-left" style="visibility: visible;">←</div><div class="system-map-gallery-images"><div class="system-map-gallery-image-cont system-map-gallery-image-cont-curr"><a href="https://cfw.sarna.net/images/systems/1.4/3151/Kufstein_3151.svg" target="_blank" class="system-map-gallery-link"><picture class="system-map-gallery-image"><source class="system-map-gallery-image-avif" type="image/avif" srcset="https://cfw.sarna.net/images/systems/1.4/avif/3151/Kufstein_3151.250.avif"><source class="system-map-gallery-image-webp" type="image/webp" srcset="https://cfw.sarna.net/images/systems/1.4/webp/3151/Kufstein_3151.250.webp"><img loading="lazy" decoding="async" class="system-map-gallery-image system-map-gallery-image-curr" src="https://cfw.sarna.net/images/systems/1.4/jpg/3151/Kufstein_3151.250.jpg"></picture><div>3151</div></a></div></div><div class="system-map-gallery-images-right" style="visibility: hidden;">→</div></div><div style="clear:both"></div></div>
            <h2><span class="mw-headline" id="Nearby_Systems">Nearby Systems</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=16" title="Edit section: Nearby Systems">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <table class="wikitable nearby-systems" style="background: #gray; text-align:center; border: 1px solid black;">
            <tbody><tr>
            <th colspan="8">Closest 44 systems (41 within 60 light-years)<br>Distance in light years, closest systems first:
            </th></tr>
            <tr>
            <td class="lessthan30"><a href="/wiki/Hohenems" title="Hohenems">Hohenems</a>
            </td>
            <td class="lessthan30">10.1
            </td>
            <td class="lessthan30"><a href="/wiki/Bushmill" title="Bushmill">Bushmill</a>
            </td>
            <td class="lessthan30">18.6
            </td>
            <td class="lessthan30"><a href="/wiki/Kandis" title="Kandis">Kandis</a>
            </td>
            <td class="lessthan30">19.9
            </td>
            <td class="lessthan30"><a href="/wiki/Feltre" title="Feltre">Feltre</a>
            </td>
            <td class="lessthan30">22.6
            </td></tr>
            <tr>
            <td class="lessthan30"><a href="/wiki/Basiliano" title="Basiliano">Basiliano</a>
            </td>
            <td class="lessthan30">25.8
            </td>
            <td class="lessthan30"><a href="/wiki/Unzmarkt" title="Unzmarkt">Unzmarkt</a>
            </td>
            <td class="lessthan30">26.0
            </td>
            <td class="lessthan30"><a href="/wiki/Tamar" title="Tamar">Tamar</a>
            </td>
            <td class="lessthan30">26.9
            </td>
            <td class="over30"><a href="/wiki/Moritz" title="Moritz">Moritz</a>
            </td>
            <td class="over30">30.5
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Engadin" title="Engadin">Engadin</a>
            </td>
            <td class="over30">31.4
            </td>
            <td class="over30"><a href="/wiki/Mozirje" title="Mozirje">Mozirje</a>
            </td>
            <td class="over30">34.5
            </td>
            <td class="over30"><a href="/wiki/Skokie" title="Skokie">Skokie</a>
            </td>
            <td class="over30">35.9
            </td>
            <td class="over30"><a href="/wiki/Stanzach" title="Stanzach">Stanzach</a>
            </td>
            <td class="over30">36.2
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Ridderkerk" title="Ridderkerk">Ridderkerk</a>
            </td>
            <td class="over30">36.5
            </td>
            <td class="over30"><a href="/wiki/Memmingen" title="Memmingen">Memmingen</a>
            </td>
            <td class="over30">37.2
            </td>
            <td class="over30"><a href="/wiki/Christiania" title="Christiania">Christiania</a>
            </td>
            <td class="over30">37.8
            </td>
            <td class="over30"><a href="/wiki/Ferleiten" title="Ferleiten">Ferleiten</a>
            </td>
            <td class="over30">38.5
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Liezen" title="Liezen">Liezen</a>
            </td>
            <td class="over30">40.0
            </td>
            <td class="over30"><a href="/wiki/Weingarten" title="Weingarten">Weingarten</a>
            </td>
            <td class="over30">41.9
            </td>
            <td class="over30"><a href="/wiki/Sevren" title="Sevren">Sevren</a>
            </td>
            <td class="over30">43.7
            </td>
            <td class="over30"><a href="/wiki/Bruben" title="Bruben">Bruben</a>
            </td>
            <td class="over30">47.4
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Planting" title="Planting">Planting</a>
            </td>
            <td class="over30">48.6
            </td>
            <td class="over30"><a href="/wiki/Thannhausen" title="Thannhausen">Thannhausen</a>
            </td>
            <td class="over30">49.4
            </td>
            <td class="over30"><a href="/wiki/Kirchbach" title="Kirchbach">Kirchbach</a>
            </td>
            <td class="over30">49.9
            </td>
            <td class="over30"><a href="/wiki/Kreller" title="Kreller">Kreller</a>
            </td>
            <td class="over30">50.0
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Laurent" title="Laurent">Laurent</a>
            </td>
            <td class="over30">50.8
            </td>
            <td class="over30"><a href="/wiki/Svarstaad" title="Svarstaad">Svarstaad</a>
            </td>
            <td class="over30">51.0
            </td>
            <td class="over30"><a href="/wiki/Dell" title="Dell">Dell</a>
            </td>
            <td class="over30">51.3
            </td>
            <td class="over30"><a href="/wiki/Volders" title="Volders">Volders</a>
            </td>
            <td class="over30">51.3
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Vorarlberg" title="Vorarlberg">Vorarlberg</a>
            </td>
            <td class="over30">51.8
            </td>
            <td class="over30"><a href="/wiki/New_Oslo" title="New Oslo">New Oslo</a>
            </td>
            <td class="over30">51.9
            </td>
            <td class="over30"><a href="/wiki/Lovinac" title="Lovinac">Lovinac</a>
            </td>
            <td class="over30">52.1
            </td>
            <td class="over30"><a href="/wiki/Rodigo" title="Rodigo">Alsace</a>
            </td>
            <td class="over30">52.3
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Dawn" title="Dawn">Dawn</a>
            </td>
            <td class="over30">52.7
            </td>
            <td class="over30"><a href="/wiki/Gunzburg" title="Gunzburg">Gunzburg</a>
            </td>
            <td class="over30">56.3
            </td>
            <td class="over30"><a href="/wiki/Predlitz" title="Predlitz">Predlitz</a>
            </td>
            <td class="over30">56.4
            </td>
            <td class="over30"><a href="/wiki/Goito" title="Goito">Goito</a>
            </td>
            <td class="over30">56.5
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/Maestu" title="Maestu">Maestu</a>
            </td>
            <td class="over30">57.8
            </td>
            <td class="over30"><a href="/wiki/Radstadt" title="Radstadt">Radstadt</a>
            </td>
            <td class="over30">58.1
            </td>
            <td class="over30"><a href="/wiki/Hermagor" title="Hermagor">Hermagor</a>
            </td>
            <td class="over30">59.0
            </td>
            <td class="over30"><a href="/wiki/Vulcan_(system)" title="Vulcan (system)">Vulcan</a>
            </td>
            <td class="over30">59.4
            </td></tr>
            <tr>
            <td class="over30"><a href="/wiki/St._John" title="St. John">St. John</a>
            </td>
            <td class="over30">59.5
            </td>
            <td class="over60"><a href="/wiki/Kobe" title="Kobe">Kobe</a>
            </td>
            <td class="over60">60.1
            </td>
            <td class="over60"><a href="/wiki/Harvest" title="Harvest">Harvest</a>
            </td>
            <td class="over60">61.2
            </td>
            <td class="over60"><a href="/wiki/Vantaa" title="Vantaa">Vantaa</a>
            </td>
            <td class="over60">62.3
            </td></tr>
            </tbody></table>
            <h2><span class="mw-headline" id="References">References</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=17" title="Edit section: References">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <div class="mw-references-wrap mw-references-columns"><ol class="references">
            <li id="cite_note-HB:HKp18-1"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp18_1-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 18: "Draconis Combine Founding" - [2319] Map</span>
            </li>
            <li id="cite_note-TP1Ep17-2"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-TP1Ep17_2-0"><span class="cite-accessibility-label">Jump up to: </span>2.0</a></sup> <sup><a href="#cite_ref-TP1Ep17_2-1">2.1</a></sup></span> <span class="reference-text"><i>The Periphery, 1st Edition</i>, p. 17: "Inner Sphere Politics"</span>
            </li>
            <li id="cite_note-HB:HKp31-3"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp31_3-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 31: "Draconis Combine after Age of War - [2571] Map"</span>
            </li>
            <li id="cite_note-4"><span class="mw-cite-backlink"><a href="#cite_ref-4" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 25: "Lyran Commonwealth after Age of War - [2571] Map"</span>
            </li>
            <li id="cite_note-HRWp159-5"><span class="mw-cite-backlink"><a href="#cite_ref-HRWp159_5-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Reunification War</i>, p. 159: "Inner Sphere - [2596] Map"</span>
            </li>
            <li id="cite_note-ER:2750p37-6"><span class="mw-cite-backlink"><a href="#cite_ref-ER:2750p37_6-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 2750</i>, p. 37: "Inner Sphere - [2750] Map"</span>
            </li>
            <li id="cite_note-7"><span class="mw-cite-backlink"><a href="#cite_ref-7" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: SLDF</i>, p. xi: "Inner Sphere - [2764] Map"</span>
            </li>
            <li id="cite_note-H:LoTv1p11-8"><span class="mw-cite-backlink"><a href="#cite_ref-H:LoTv1p11_8-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Liberation of Terra Volume 1</i>, p. 11: "Inner Sphere - [2765] Map"</span>
            </li>
            <li id="cite_note-FSW.28SB.29p25-9"><span class="mw-cite-backlink"><a href="#cite_ref-FSW.28SB.29p25_9-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>First Succession War</i>, p. 25: "Inner Sphere - [2786] Map"</span>
            </li>
            <li id="cite_note-HB:HKp43-10"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp43_10-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 43: "Draconis Combine after First Succession War - [2822] Map"</span>
            </li>
            <li id="cite_note-11"><span class="mw-cite-backlink"><a href="#cite_ref-11" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 36: "Lyran Commonwealth after First Succession War - [2822] Map"</span>
            </li>
            <li id="cite_note-H:LOTV2p122-12"><span class="mw-cite-backlink"><a href="#cite_ref-H:LOTV2p122_12-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Liberation of Terra Volume 2</i>, pp. 122–123: "Inner Sphere - [2822] Map"</span>
            </li>
            <li id="cite_note-FSW.28SB.29p113-13"><span class="mw-cite-backlink"><a href="#cite_ref-FSW.28SB.29p113_13-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>First Succession War</i>, p. 113: "Inner Sphere - [2822] Map"</span>
            </li>
            <li id="cite_note-HB:HKp53-14"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp53_14-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 53: "Draconis Combine after Second Succession War - [2864] Map"</span>
            </li>
            <li id="cite_note-15"><span class="mw-cite-backlink"><a href="#cite_ref-15" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 40: "Lyran Commonwealth after Second Succession War - [2864] Map"</span>
            </li>
            <li id="cite_note-16"><span class="mw-cite-backlink"><a href="#cite_ref-16" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>House Kurita (The Draconis Combine)</i>, foldout: "Draconis Combine Map - [3025]"</span>
            </li>
            <li id="cite_note-HB:HKp64-17"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp64_17-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 64: "Draconis Combine after Third Succession War - [3025] Map"</span>
            </li>
            <li id="cite_note-18"><span class="mw-cite-backlink"><a href="#cite_ref-18" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>House Steiner (The Lyran Commonwealth)</i>, foldout: "Lyran Commonwealth Map - [3025]"</span>
            </li>
            <li id="cite_note-HB:HSp47-19"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HSp47_19-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 47: "Lyran Commonwealth after Third Succession War - [3025] Map"</span>
            </li>
            <li id="cite_note-FSWMAv2p60-20"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-FSWMAv2p60_20-0"><span class="cite-accessibility-label">Jump up to: </span>20.0</a></sup> <sup><a href="#cite_ref-FSWMAv2p60_20-1">20.1</a></sup></span> <span class="reference-text"><i>NAIS The Fourth Succession War Military Atlas Volume 2</i>, p. 60: "Draconis Front Map (January 3029)"</span>
            </li>
            <li id="cite_note-FSWMAv2p61-21"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-FSWMAv2p61_21-0"><span class="cite-accessibility-label">Jump up to: </span>21.0</a></sup> <sup><a href="#cite_ref-FSWMAv2p61_21-1">21.1</a></sup> <sup><a href="#cite_ref-FSWMAv2p61_21-2">21.2</a></sup></span> <span class="reference-text"><i>NAIS The Fourth Succession War Military Atlas Volume 2</i>, p. 61: "Draconis Front Map (January 3030)"</span>
            </li>
            <li id="cite_note-FSWMAv2p93-22"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-FSWMAv2p93_22-0"><span class="cite-accessibility-label">Jump up to: </span>22.0</a></sup> <sup><a href="#cite_ref-FSWMAv2p93_22-1">22.1</a></sup></span> <span class="reference-text"><i>NAIS The Fourth Succession War Military Atlas Volume 2</i>, pp. 93–94: "End Of The War"</span>
            </li>
            <li id="cite_note-23"><span class="mw-cite-backlink"><a href="#cite_ref-23" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 56: "Lyran Commonwealth after Fourth Succession War - [3030] Map"</span>
            </li>
            <li id="cite_note-HB:HKp66-24"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp66_24-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 66: "Draconis Combine after Fourth Succession War - [3030] Map"</span>
            </li>
            <li id="cite_note-H:BWp103-25"><span class="mw-cite-backlink"><a href="#cite_ref-H:BWp103_25-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: Brush Wars</i>, p. 103: "Free Rasalhague Republic map 3034"</span>
            </li>
            <li id="cite_note-26"><span class="mw-cite-backlink"><a href="#cite_ref-26" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Historical: War of 3039</i>, p. 133: "Inner Sphere - [3040] Map"</span>
            </li>
            <li id="cite_note-HB:HKp68-27"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp68_27-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 68: "Draconis Combine after War of 3039 - [3040] Map"</span>
            </li>
            <li id="cite_note-28"><span class="mw-cite-backlink"><a href="#cite_ref-28" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 56: "Lyran Commonwealth after War of 3039 - [3040] Map"</span>
            </li>
            <li id="cite_note-ER3052p11-29"><span class="mw-cite-backlink"><a href="#cite_ref-ER3052p11_29-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3052</i>, p. 11: "Inner Sphere - 3050"</span>
            </li>
            <li id="cite_note-ER3052p23-30"><span class="mw-cite-backlink"><a href="#cite_ref-ER3052p23_30-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3052</i>, p. 23: "Inner Sphere - [3052] Map"</span>
            </li>
            <li id="cite_note-31"><span class="mw-cite-backlink"><a href="#cite_ref-31" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 61: "Lyran Commonwealth after Clan Invasion - [3052] Map"</span>
            </li>
            <li id="cite_note-HB:HKp71-32"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp71_32-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 71: "Draconis Combine after Operation REVIVAL - [3052] Map"</span>
            </li>
            <li id="cite_note-33"><span class="mw-cite-backlink"><a href="#cite_ref-33" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3062</i>, p. 11: "Inner Sphere - [3057] Map"</span>
            </li>
            <li id="cite_note-34"><span class="mw-cite-backlink"><a href="#cite_ref-34" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3062</i>, p. 29: "Inner Sphere - [3063] Map"</span>
            </li>
            <li id="cite_note-HB:HKp74-35"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HKp74_35-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Kurita</i>, p. 74: "Draconis Combine after FedCom Civil War - [3067] Map"</span>
            </li>
            <li id="cite_note-36"><span class="mw-cite-backlink"><a href="#cite_ref-36" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Jihad: Final Reckoning</i>, p. 43: "Inner Sphere - [3067] Map"</span>
            </li>
            <li id="cite_note-HB:HSp70-37"><span class="mw-cite-backlink"><a href="#cite_ref-HB:HSp70_37-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Handbook: House Steiner</i>, p. 70: "Lyran Alliance after FedCom Civil War - [3067] Map"</span>
            </li>
            <li id="cite_note-38"><span class="mw-cite-backlink"><a href="#cite_ref-38" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Jihad Secrets: The Blake Documents</i>, p. 65: "Inner Sphere - [3075] Map"</span>
            </li>
            <li id="cite_note-FR:Cp27-39"><span class="mw-cite-backlink"><a href="#cite_ref-FR:Cp27_39-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Report: Clans</i>, p. 27: "Clan Wolf/Clan Hell's Horses Deployment Map - [August 3079]"</span>
            </li>
            <li id="cite_note-FR:DCMSp21-40"><span class="mw-cite-backlink"><a href="#cite_ref-FR:DCMSp21_40-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Report: DCMS</i>, p. 21: "Draconis Combine Mustered Soldiery Deployment Map - [August 3079]"</span>
            </li>
            <li id="cite_note-41"><span class="mw-cite-backlink"><a href="#cite_ref-41" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Jihad: Final Reckoning</i>, p. 63: "Inner Sphere - [3081] Map"</span>
            </li>
            <li id="cite_note-42"><span class="mw-cite-backlink"><a href="#cite_ref-42" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: 3085</i>, p. 127: "Inner Sphere - [3085] Map"</span>
            </li>
            <li id="cite_note-ER:3145p11-43"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3145p11_43-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3145</i>, p. 11: "Inner Sphere - [3135] Map"</span>
            </li>
            <li id="cite_note-ER:3145p39-44"><span class="mw-cite-backlink"><a href="#cite_ref-ER:3145p39_44-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Era Report: 3145</i>, p. 39: "Inner Sphere - [3145] Map"</span>
            </li>
            <li id="cite_note-FM:3145pVI-45"><span class="mw-cite-backlink"><a href="#cite_ref-FM:3145pVI_45-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: 3145</i>, p. VI: "Inner Sphere - [3145] Map"</span>
            </li>
            <li id="cite_note-SFp102-103-46"><span class="mw-cite-backlink"><a href="#cite_ref-SFp102-103_46-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Shattered Fortress</i>, pp. 102–103: "Inner Sphere - [3151] Map"</span>
            </li>
            <li id="cite_note-FSWp62-47"><span class="mw-cite-backlink"><a href="#cite_ref-FSWp62_47-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>First Succession War</i>, p. 62: "The War of Raids: House Steiner's Succession War"</span>
            </li>
            <li id="cite_note-NAIS4SWp44-48"><span class="mw-cite-backlink"><a href="#cite_ref-NAIS4SWp44_48-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>NAIS The Fourth Succession War Military Atlas Volume 1</i>, pp. 44–45: "Operation GÖTTERDÄMMERUNG"</span>
            </li>
            <li id="cite_note-FSWMAv1p92-49"><span class="mw-cite-backlink"><a href="#cite_ref-FSWMAv1p92_49-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>NAIS The Fourth Succession War Military Atlas Volume 1</i>, p. 92: "Conclusion"</span>
            </li>
            <li id="cite_note-50"><span class="mw-cite-backlink"><a href="#cite_ref-50" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Wolf Clan Sourcebook</i>, p. 62</span>
            </li>
            <li id="cite_note-FM2765:LCp9-51"><span class="mw-cite-backlink"><a href="#cite_ref-FM2765:LCp9_51-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Report 2765: LCAF</i>, p. 9: "Regimental Status"</span>
            </li>
            <li id="cite_note-FSWp139-52"><span class="mw-cite-backlink"><a href="#cite_ref-FSWp139_52-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>First Succession War</i>, p. 139: "First Succession War Deployment Table - LCAF"</span>
            </li>
            <li id="cite_note-FSWp137-53"><span class="mw-cite-backlink"><a href="#cite_ref-FSWp137_53-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>First Succession War</i>, p. 137: "First Succession War Deployment Table - DCMS"</span>
            </li>
            <li id="cite_note-WCS062-54"><span class="mw-cite-backlink">↑ <sup><a href="#cite_ref-WCS062_54-0"><span class="cite-accessibility-label">Jump up to: </span>54.0</a></sup> <sup><a href="#cite_ref-WCS062_54-1">54.1</a></sup> <sup><a href="#cite_ref-WCS062_54-2">54.2</a></sup> <sup><a href="#cite_ref-WCS062_54-3">54.3</a></sup> <sup><a href="#cite_ref-WCS062_54-4">54.4</a></sup></span> <span class="reference-text"><i>Wolf Clan Sourcebook</i>, p. 62</span>
            </li>
            <li id="cite_note-FM:3145p172-55"><span class="mw-cite-backlink"><a href="#cite_ref-FM:3145p172_55-0" aria-label="Jump up" title="Jump up">↑</a></span> <span class="reference-text"><i>Field Manual: 3145</i>, p. 172: "Clan Force Deployments - Rasalhague Dominion"</span>
            </li>
            </ol></div>
            <h2><span class="mw-headline" id="Bibliography">Bibliography</span><span class="mw-editsection"><span class="mw-editsection-bracket">[</span><a href="/wiki/index.php?title=Kufstein&amp;action=edit&amp;section=18" title="Edit section: Bibliography">edit</a><span class="mw-editsection-bracket">]</span></span></h2>
            <div class="desktop-3-col">
            <ul><li><i><a href="/wiki/Era_Report:_2750" title="Era Report: 2750">Era Report: 2750</a></i></li>
            <li><i><a href="/wiki/Era_Report:_3052" title="Era Report: 3052">Era Report: 3052</a></i></li>
            <li><i><a href="/wiki/Era_Report:_3062" title="Era Report: 3062">Era Report: 3062</a></i></li>
            <li><i><a href="/wiki/Era_Report:_3145" title="Era Report: 3145">Era Report: 3145</a></i></li>
            <li><i><a href="/wiki/Field_Manual:_3085" title="Field Manual: 3085">Field Manual: 3085</a></i></li>
            <li><i><a href="/wiki/Field_Manual:_3145" title="Field Manual: 3145">Field Manual: 3145</a></i></li>
            <li><i><a href="/wiki/Field_Manual:_SLDF" title="Field Manual: SLDF">Field Manual: SLDF</a></i></li>
            <li><i><a href="/wiki/Field_Report:_Clans" title="Field Report: Clans">Field Report: Clans</a></i></li>
            <li><i><a href="/wiki/Field_Report:_DCMS" title="Field Report: DCMS">Field Report: DCMS</a></i></li>
            <li><i><a href="/wiki/Field_Report_2765:_LCAF" title="Field Report 2765: LCAF">Field Report 2765: LCAF</a></i></li>
            <li><i><a href="/wiki/First_Succession_War_(sourcebook)" title="First Succession War (sourcebook)">First Succession War</a></i></li>
            <li><i><a href="/wiki/Handbook:_House_Kurita" title="Handbook: House Kurita">Handbook: House Kurita</a></i></li>
            <li><i><a href="/wiki/Handbook:_House_Steiner" title="Handbook: House Steiner">Handbook: House Steiner</a></i></li>
            <li><i><a href="/wiki/Historical:_Liberation_of_Terra_Volume_1" title="Historical: Liberation of Terra Volume 1">Historical: Liberation of Terra Volume 1</a></i></li>
            <li><i><a href="/wiki/Historical:_Liberation_of_Terra_Volume_2" title="Historical: Liberation of Terra Volume 2">Historical: Liberation of Terra Volume 2</a></i></li>
            <li><i><a href="/wiki/Historical:_Reunification_War" title="Historical: Reunification War">Historical: Reunification War</a></i></li>
            <li><i><a href="/wiki/Historical:_War_of_3039" title="Historical: War of 3039">Historical: War of 3039</a></i></li>
            <li><i><a href="/wiki/House_Kurita_(The_Draconis_Combine)" title="House Kurita (The Draconis Combine)">House Kurita (The Draconis Combine)</a></i></li>
            <li><i><a href="/wiki/House_Steiner_(The_Lyran_Commonwealth)" title="House Steiner (The Lyran Commonwealth)">House Steiner (The Lyran Commonwealth)</a></i></li>
            <li><i><a href="/wiki/Jihad:_Final_Reckoning" title="Jihad: Final Reckoning">Jihad: Final Reckoning</a></i></li>
            <li><i><a href="/wiki/Jihad_Secrets:_The_Blake_Documents" title="Jihad Secrets: The Blake Documents">Jihad Secrets: The Blake Documents</a></i></li>
            <li><i><a href="/wiki/NAIS_The_Fourth_Succession_War_Military_Atlas_Volume_1" title="NAIS The Fourth Succession War Military Atlas Volume 1">NAIS The Fourth Succession War Military Atlas Volume 1</a></i></li>
            <li><i><a href="/wiki/NAIS_The_Fourth_Succession_War_Military_Atlas_Volume_2" title="NAIS The Fourth Succession War Military Atlas Volume 2">NAIS The Fourth Succession War Military Atlas Volume 2</a></i></li>
            <li><i><a href="/wiki/The_Periphery_(sourcebook)" title="The Periphery (sourcebook)">The Periphery, First Edition</a></i></li>
            <li><i><a href="/wiki/Shattered_Fortress" title="Shattered Fortress">Shattered Fortress</a></i></li>
            <li><i><a href="/wiki/Wolf_Clan_Sourcebook" title="Wolf Clan Sourcebook">Wolf Clan Sourcebook</a></i></li></ul>
            </div>
            <!-- 
            NewPP limit report
            Cached time: 20240209230713
            Cache expiry: 2592000
            Dynamic content: false
            Complications: []
            [SMW] In‐text annotation parser time: 0 seconds
            CPU time usage: 0.189 seconds
            Real time usage: 0.486 seconds
            Preprocessor visited node count: 1165/1000000
            Preprocessor generated node count: 0/1000000
            Post‐expand include size: 7077/2097152 bytes
            Template argument size: 735/2097152 bytes
            Highest expansion depth: 8/40
            Expensive parser function count: 0/200
            Unstrip recursion depth: 0/20
            Unstrip post‐expand size: 21852/5000000 bytes
            Lua time usage: 0.220/7 seconds
            Lua virtual size: 9.73 MB/50 MB
            Lua estimated memory usage: 0 bytes
            -->
            <!--
            Transclusion expansion time report (%,ms,calls,template)
            100.00%  376.770      1 -total
             77.12%  290.554      2 Template:Infobox
             44.46%  167.519      1 Template:InfoBoxSystem
             35.06%  132.106      1 Template:InfoBoxPlanet
              5.64%   21.246      1 Template:Div_col
              1.84%    6.937      1 Template:E
              1.44%    5.419      1 Template:Scrollbox
              1.05%    3.940      2 Template:Template_other
              1.00%    3.785      1 Template:Div_col_end
              0.96%    3.608      1 Template:Main_other
            -->
            
            <!-- Saved in parser cache with key sarna_wiki-wiki_:pcache:idhash:2402-0!canonical and timestamp 20240209230713 and revision id 1041607
             -->
            </div><h2></h2></div><div class="printfooter">
            Retrieved from "<a dir="ltr" href="https://www.sarna.net/wiki/index.php?title=Kufstein&amp;oldid=1041607">https://www.sarna.net/wiki/index.php?title=Kufstein&amp;oldid=1041607</a>"</div>
                        <div id="catlinks" class="catlinks" data-mw="interface"><div id="mw-normal-catlinks" class="mw-normal-catlinks"><a href="/wiki/Special:Categories" title="Special:Categories">Categories</a>: <ul><li><a href="/wiki/Category:Systems" title="Category:Systems">Systems</a></li><li><a href="/wiki/Category:Planets" title="Category:Planets">Planets</a></li><li><a href="/wiki/Category:Clan_Wolf_Systems" title="Category:Clan Wolf Systems">Clan Wolf Systems</a></li><li><a href="/wiki/Category:Draconis_Combine_Systems" title="Category:Draconis Combine Systems">Draconis Combine Systems</a></li><li><a href="/wiki/Category:Free_Rasalhague_Republic_Systems" title="Category:Free Rasalhague Republic Systems">Free Rasalhague Republic Systems</a></li><li><a href="/wiki/Category:Lyran_Commonwealth_Systems" title="Category:Lyran Commonwealth Systems">Lyran Commonwealth Systems</a></li><li><a href="/wiki/Category:Principality_of_Rasalhague_Systems" title="Category:Principality of Rasalhague Systems">Principality of Rasalhague Systems</a></li><li><a href="/wiki/Category:Rasalhague_Dominion_Systems" title="Category:Rasalhague Dominion Systems">Rasalhague Dominion Systems</a></li></ul></div><div id="mw-hidden-catlinks" class="mw-hidden-catlinks mw-hidden-cats-hidden">Hidden categories: <ul><li><a href="/wiki/Category:Systems_with_undetermined_Spectral_class" title="Category:Systems with undetermined Spectral class">Systems with undetermined Spectral class</a></li><li><a href="/wiki/Category:Systems_with_undetermined_Recharge_time" title="Category:Systems with undetermined Recharge time">Systems with undetermined Recharge time</a></li><li><a href="/wiki/Category:Systems_with_undetermined_number_of_Recharge_stations" title="Category:Systems with undetermined number of Recharge stations">Systems with undetermined number of Recharge stations</a></li><li><a href="/wiki/Category:Systems_with_undetermined_number_of_Planets" title="Category:Systems with undetermined number of Planets">Systems with undetermined number of Planets</a></li><li><a href="/wiki/Category:Articles_using_infobox_templates_with_no_data_rows" title="Category:Articles using infobox templates with no data rows">Articles using infobox templates with no data rows</a></li></ul></div></div>            <!-- end content -->
                                    <div class="visualClear"></div>
                    </div>
               </div>
            </div>
            """;

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(27);
        actual.CountFactions("No record").Should().Be(3);
        actual.CountFactions("Lyran Commonwealth").Should().Be(5);
        actual.CountFactions("Principality of Rasalhague").Should().Be(1);
        actual.CountFactions("Draconis Combine").Should().Be(4);
        actual.CountFactions("Draconis Combine (January)").Should().Be(1);
        actual.CountFactions("Lyran Commonwealth (January)").Should().Be(1);
        actual.CountFactions("Free Rasalhague Republic").Should().Be(3);
        actual.CountFactions("Clan Wolf").Should().Be(9);
        actual.CountFactions("Rasalhague Dominion").Should().Be(2);
    }

    /// <summary>
    /// https://www.sarna.net/wiki/New_Avalon
    /// </summary>
    [Fact]
    public void Should_ParseSystemNewAvalon()
    {
        var page = File.ReadAllText("./pages/New_Avalon.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(30);
    }

    /// <summary>
    /// https://sarna.net/wiki/Luyten_68-28
    /// </summary>
    [Fact]
    public void Should_ParseSystemLuyten6828()
    {
        var page = File.ReadAllText("./pages/Luyten_68-28.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(0);
    }

    /// <summary>
    /// https://www.sarna.net/wiki/Ingress
    /// </summary>
    [Fact]
    public void Should_ParseSystemIngress()
    {
        var page = File.ReadAllText("./pages/Ingress.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(35);
        actual.CountFactions("Terran Alliance").Should().Be(1);
    }

    /// <summary>
    /// https://www.sarna.net/wiki/Londerholm
    /// </summary>
    [Fact]
    public void Should_ParseSystemLonderholm()
    {
        var page = File.ReadAllText("./pages/Londerholm.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(10);
        actual.CountFactions("The Clans").Should().Be(1);
        actual.CountFactions("Clan Smoke Jaguar").Should().Be(4);
        actual.CountFactions("Clan Coyote").Should().Be(5);
        actual.CountFactions("Clan Ice Hellion").Should().Be(4);
        actual.CountFactions("Clan Nova Cat").Should().Be(1);
        actual.CountFactions("Clan Stone Lion").Should().Be(1);
    }

    /// <summary>
    /// https://www.sarna.net/wiki/Tathis
    /// </summary>
    [Fact]
    public void Should_ParseSystemTathis()
    {
        var page = File.ReadAllText("./pages/Tathis.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(9);
        actual.CountFactions("Clan Mongoose").Should().Be(1);
        actual.CountFactions("Clan Diamond Shark").Should().Be(5);
        actual.CountFactions("Clan Ice Hellion").Should().Be(4);
        actual.CountFactions("Clan Coyote").Should().Be(2);
        actual.CountFactions("Clan Ice Hellion (Abandoned)").Should().Be(1);
        actual.CountFactions("Clan Cloud Cobra").Should().Be(1);
        actual.CountFactions("Clan Star Adder").Should().Be(8);
    }
    
    /// <summary>
    /// https://www.sarna.net/wiki/Chapultepec
    /// </summary>
    [Fact]
    public void Should_ParseSystemChapultepec()
    {
        var page = File.ReadAllText("./pages/Chapultepec.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(29);
    }
    
    /// <summary>
    /// https://www.sarna.net/wiki/Rochelle
    /// </summary>
    [Fact]
    public void Should_ParseSystemRochelle()
    {
        var page = File.ReadAllText("./pages/Rochelle.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(39);
    }
    
    /// <summary>
    /// https://www.sarna.net/wiki/Shadow
    /// </summary>
    [Fact]
    public void Should_ParseSystemShadow()
    {
        var page = File.ReadAllText("./pages/Shadow.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(13);
    }
    
    /// <summary>
    /// https://www.sarna.net/wiki/Rocky
    /// </summary>
    [Fact]
    public void Should_ParseSystemRocky()
    {
        var page = File.ReadAllText("./pages/Rocky.html");

        var actual = BattleTechHtmlParser.FindPoliticalAffiliations(page);

        actual.Count.Should().Be(27);
    }
}