/* eslint-disable camelcase */

exports.shorthands = undefined;

exports.up = pgm => {
    pgm.createTable("activity_registration", {
        id: {
            type: "uuid",
            primaryKey: true,
            default: pgm.func("uuid_generate_v1()"),
        },
        responsible_name: {
            type: "string",
            notNull: true,
        },
        responsible_number: {
            type: "string",
            notNull: true,
        },
        responsible_email: {
            type: "string",
            notNull: true,
        },
        co_responsible_name: {
            type: "string",
        },
        co_responsible_number: {
            type: "string",
        },
        co_responsible_email: {
            type: "string",
        },
        serving_permit: {
            type: "boolean",
            notNull: true,
            default: false,
        },
        accepted: {
            type: "boolean",
            notNull: true,
            default: false,
        },
        in_progress: {
            type: "boolean",
            notNull: true,
            default: true,
        },
    });
};

exports.down = pgm => {};
