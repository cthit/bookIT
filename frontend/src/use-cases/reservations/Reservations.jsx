import React from "react";
import FullCallendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from "@fullcalendar/timegrid";
import { DigitDesign, DigitButtonGroup } from "@cthit/react-digit-components";
import NewReservationFrom from "../new-reservation/NewReservation.form";
import { Redirect } from "react-router-dom";
import { useState } from "react";

const Reservations = () => {
    const [redirect, setRedirect] = useState(null);
    return (
        <div
            style={{
                display: "flex",
                justifyContent: "center",
                flexDirection: "column",
                width: "95%",
            }}
        >
            {redirect && <Redirect to={redirect} />}
            <DigitButtonGroup
                primary
                outlined
                buttons={[
                    {
                        text: "Skapa bokning",
                        onClick: () => setRedirect("/reservations/new"),
                    },
                    {
                        text: "Dina bokningar",
                    },
                ]}
            />
            <FullCallendar
                eventTimeFormat={{
                    hour: "2-digit",
                    minute: "2-digit",
                    hour12: false,
                }}
                slotLabelFormat={{
                    hour: "2-digit",
                    minute: "2-digit",
                    hour12: false,
                }}
                rerenderDelay={1000}
                allDaySlot
                weekNumbers
                editable
                headerToolbar={{
                    start: "title",
                    center: "",
                    end: "today,prev,next",
                }}
                plugins={[dayGridPlugin, timeGridPlugin]}
                initialView="timeGridWeek"
                eventOverlap
                events={[
                    {
                        title: "Styrit Möte",
                        start: "2020-08-29T06:00",
                        end: "2020-08-29T07:00",
                        display: "block",
                    },
                    {
                        title: "Styrit Möte",
                        start: "2020-08-29T12:00",
                        end: "2020-08-29T13:00",
                        display: "block",
                    },
                    {
                        title: "Styrit Möte",
                        start: "2020-08-29T12:30",
                        end: "2020-08-29T13:30",
                        display: "block",
                    },
                ]}
            />
        </div>
    );
};

export default Reservations;