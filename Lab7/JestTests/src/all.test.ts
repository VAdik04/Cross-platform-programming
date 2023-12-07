import { controllerTest } from "./api";


controllerTest("MovementLocations", "shipmentLocationId", {
    shipmentId: 73,
    fromLocationId: 11,
    toLocationId: 55,
    dateStarted: '2023-05-18T12:22:42.859927+03:00',
    dateCompleted: '2023-12-07T17:05:22.017883+02:00',
    otherDetails: 'Rerum et eveniet.'
});

controllerTest("OrganizationTypes", "organizationTypeCode", {
    organizationTypeCode: Math.random().toString(),
    organizationTypeDescription: "organizationTypeDescription"
});

controllerTest("ProductAndServiceTypes", "productSvcTypeCode", {
    productSvcTypeCode: Math.random().toString(),
    productSvcTypeDescription: "productSvcTypeDescription"
});

controllerTest("Shipments", "shipmentId", {
    fromOrganizationId: 33,
    toOrganizationId: 59,
    shipmentDetails: 'Quis ut deleniti minus temporibus aut id excepturi.'
});

controllerTest("Countries", "countryCode", {
    countryCode: Math.random().toString(),
    countryName: 'Sudan',
    countryCurrency: 'MAD',
    languagesSpoken: 'maroon,Toys,West Virginia,SAS',
    usdExchangeRate: 349.22,
    usdExchangeDate: '2023-11-25T22:06:12.596327+07:00'
});