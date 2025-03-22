import React from "react";
import "./AboutUs.css";
// Dodaj odgovarajuću sliku u assets folder

const AboutUs = () => {
  return (
    <div id="about-us" className="about-us-container">
      <div className="about-us-content">
        <div className="about-us-image">
          <img src="onama.jpg" alt="Lider Travel" />
        </div>
        <div className="about-us-text">
          <h2>O nama</h2>
          <p>
            Mi, u turističkoj agenciji <strong>Ekspedicija</strong>, znamo da u
            životu svake osobe bitan deo zauzimaju putovanja i zbog toga naš tim
            vredno radi na tome da ona budu profesionalno organizovana i
            osmišljena do najsitnijih detalja. Baš ti detalji se pamte i
            prepričavaju, a vremenom postaju najlepše uspomene. Te pozitivne
            uspomene više desetina hiljada putnika naša su najveća referenca u
            turizmu. One odvajaju Ekspedicija travel putovanja od ostalih putovanja.
          </p>
          <p>
            Znamo koliko vredno radite da biste sebi omogućili odmor i zato nam
            je jako bitno da budete zadovoljni našim uslugama. Pridajemo veliku
            pažnju svakoj mušteriji i zato imamo veliki procenat putnika koji
            godinama putuju sa nama.
          </p>
          <p>
            Na destinacijama imamo veliki broj angažovanih ljudi koji rade da bi
            se putnici osećali sigurno i bezbrižno tokom boravka, 24 časa dnevno.
            Mladi, edukovani vodiči, koordinatori, animatori, dj-evi, fotografi,
            medicinska podrška, tehnička podrška, asistenti realizacije,
            predstavnici agencije na terenu su samo neki od ljudi koji se trude
            da Vaš odmor protekne bez ikakvih nepredviđenih okolnosti.
          </p>
          <p>
            Turistička agencija Ekspedicija kao najveći turistički
            brend na ovim prostorima u svom portfoliju potpisuje letovanja avio i
            autobuskim prevozom, ekskurzije, zimovanja, doček Nove godine,
            putovanja za mlade...
          </p>
        </div>
      </div>
    </div>
  );
};

export default AboutUs;